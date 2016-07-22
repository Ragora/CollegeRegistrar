using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrarConsole
{
    public partial class FormMain
    {
        /// <summary>
        /// Class representing a time slot, ideally open, for the given room on a given day.
        /// </summary>
        private class TimeSlot
        {
            /// <summary>
            /// The room this time slot is for.
            /// </summary>
            public Room TargetRoom;

            /// <summary>
            /// When the room is open.
            /// </summary>
            public DateTime Start;

            /// <summary>
            /// When the room's open time slot ends.
            /// </summary>
            public DateTime End;
        };

        private List<Schedule> CurrentSchedules;

        /// <summary>
        /// A dictionary mapping course names to the number of students we are trying to assign to each course.
        /// </summary>
        private Dictionary<string, int> StudentCounts;

        /// <summary>
        /// A list of currently unscheduled students.
        /// </summary>
        private List<Student> UnscheduledStudents;

        private Dictionary<int, List<Course>> UnscheduledCourses;

        private void UpdateProgress(string text)
        {
            ListBoxUpdateProgress.Items.Add(text);
            ListBoxUpdateProgress.TopIndex = ListBoxUpdateProgress.Items.Count - 1;
            Refresh();
        }

        /// <summary>
        /// Initializes the hardcoded course data as our database currently doesn't store any of it.
        /// </summary>
        private void InitializeHardcodedData()
        {
            if (StudentCounts == null)
            {
                StudentCounts = new Dictionary<string, int>();

                // CP
                StudentCounts["SSD551"] = 45;
                StudentCounts["ITP201"] = 35;
                StudentCounts["SSD750"] = 20;
                StudentCounts["ITP300"] = 35;

                // MMA
                StudentCounts["MMA113"] = 42;
                StudentCounts["MMA144"] = 42;
                StudentCounts["MMA131"] = 30;
                StudentCounts["MMA280"] = 80;

                // CAD
                StudentCounts["CAD120"] = 30;
                StudentCounts["CAD125"] = 25;
                StudentCounts["CAD131"] = 25;
                StudentCounts["CAD145"] = 40;

                // GenEd
                StudentCounts["GEE150"] = 150;
                StudentCounts["GEE211"] = 109;
                StudentCounts["GEH120"] = 75;
                StudentCounts["GES100"] = 125;
            }
        }

        /// <summary>
        /// Returns a list of time slots available for the given course under the restrictions that it should be a lab room,
        /// class room, instructional room, etc.
        /// </summary>
        /// <param name="course">
        /// The course to account for.
        /// </param>
        /// <param name="labRoom">
        /// Should the room be a lab room?
        /// </param>
        /// <param name="classRoom">
        /// Should the room be a class room?
        /// </param>
        /// <param name="instructRoom">
        /// Should the room be an instructional room?
        /// </param>
        /// <returns>
        /// A list of time slots representing available times at what rooms.
        /// </returns>
        private List<TimeSlot> RequestAvailableRooms(Course course, bool labRoom, bool classRoom, bool instructRoom, bool library)
        {
            List<TimeSlot> availableSlots = new List<TimeSlot>();

            foreach (Room room in Program.Database.Rooms)
            {
                if ((room.RoomID != "Lib" && library) && (!room.Resource.Cad && course.Requirement.Cad) && (!room.Resource.Program && course.Requirement.Program)
                && (!room.Resource.Multi && course.Requirement.Multi) && (!room.Resource.Gaming && course.Requirement.Gaming))
                continue;

                if ((labRoom && !room.Resource.Lab) || (classRoom && !room.Resource.Classroom) || (instructRoom && !room.Resource.Instruct) || (library && !room.RoomID.Contains("Lib")))
                    continue;

                // Figure out where existing schedule usages are
                var usages = from schedule in CurrentSchedules
                             where schedule.RoomID == room.RoomID
                             select schedule;

                // We don't care so much about what's already here, just flag it
                RangedList<Room> usedSlots = new RangedList<Room>();
                usedSlots.Payload = usedSlots.Payload.Distinct().ToList();
                foreach (Schedule schedule in usages)
                {
                    usedSlots.AddRange(schedule.StartTime, schedule.EndTime, room);
                }

                // We start on monday and blow through each day of the week, looking in 1-hour increments for available slots
                DateTime startDate = StartDate();

                for (int day = 0; day < 5; day++)
                {
                    DateTime currentDate = startDate.AddDays(day).AddHours(8.335);
                    DateTime? openStart = currentDate;

                    for (int hour = 0; hour < 8; hour++)
                    {
                        // Is the current date available?
                        RangedList<Room>.PayloadIndex currentSlot = usedSlots.GetIndex(currentDate);

                        if (currentSlot != null) 
                        {
                            // We've got to close an availability block
                            if (openStart != null && (DateTime)openStart != currentDate)
                            {
                                DateTime start = (DateTime)openStart;
                                DateTime end = currentDate;

                                // Record our availability range
                                TimeSlot newSlot = new TimeSlot()
                                {
                                    Start = start,
                                    End = end,
                                    TargetRoom = room,
                                };

                                availableSlots.Add(newSlot);
                            }

                            openStart = null; // Not available, move along to the next hour
                        }
                        else
                            if (openStart == null)
                                openStart = currentDate;                        

                        currentDate = currentDate.AddHours(1);
                    }

                    // This will happen on our first pass because entire days are still available
                    if (openStart != null && (DateTime)openStart != currentDate)
                    {
                        DateTime start = (DateTime)openStart;
                        DateTime end = currentDate;

                        // Record our availability range
                        TimeSlot newSlot = new TimeSlot()
                        {
                            Start = start,
                            End = end,
                            TargetRoom = room,
                        };

                        availableSlots.Add(newSlot);
                    }
                }
            }

            List<TimeSlot> result = availableSlots.Distinct().ToList();
            return result;
        }

        /// <summary>
        /// Finds the largest room in the list, trying to keep it trimmed to as close to studentCount as possible.
        /// </summary>
        /// <param name="slots">
        /// The time slots to process.
        /// </param>
        /// <param name="studentCount">
        /// The number of students to try and schedule.
        /// </param>
        /// <param name="hours">
        /// The number of hours to try and schedule for.
        /// </param>
        /// <returns></returns>
        TimeSlot FindLargestRoom(List<TimeSlot> slots, int studentCount, int hours)
        {
            TimeSlot result = null;

            // No slots to check
            if (slots.Count == 0)
                return null;

            // First we just look for the biggest room period that can work in these hours
            foreach (TimeSlot timeslot in slots)
            {
                if ((result == null || timeslot.TargetRoom.Capacity > result.TargetRoom.Capacity) && timeslot.End.Hour - timeslot.Start.Hour >= hours)
                    result = timeslot;
            }

            // Now compared to this, we try to find a room that's at least capable of holding studentCount, unless the
            // existing room already can't
            if (result.TargetRoom.Capacity < studentCount)
                return result;

            TimeSlot largestRoom = result;

            // If we've gotten here, then we have a chance that there's a smaller room that's sufficient
            foreach (TimeSlot timeslot in slots)
                if (timeslot.TargetRoom.Capacity < largestRoom.TargetRoom.Capacity && timeslot.TargetRoom.Capacity >= studentCount && timeslot.End.Hour - timeslot.Start.Hour >= hours)
                    result = timeslot;

            // Just return whatever we have at this point
            return result;
        }

        /// <summary>
        /// Returns a date time representing when the schedules should start, starting at the next Monday
        /// relative to current system time.
        /// </summary>
        /// <returns>
        /// A date time on the next monday.
        /// </returns>
        DateTime StartDate()
        {
            DateTime now = DateTime.Now;

            DateTime result = new DateTime();
            result = result.AddYears(now.Year - 1);
            result = result.AddMonths(now.Month - 1);
            result = result.AddDays(now.Day - 1);

            // Wrap around to the first monday
            switch (result.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    break;
                case DayOfWeek.Tuesday:
                    result = result.AddDays(6);
                    break;
                case DayOfWeek.Wednesday:
                    result = result.AddDays(5);
                    break;
                case DayOfWeek.Thursday:
                    result = result.AddDays(4);
                    break;
                case DayOfWeek.Friday:
                    result = result.AddDays(3);
                    break;
                case DayOfWeek.Saturday:
                    result = result.AddDays(2);
                    break;
                case DayOfWeek.Sunday:
                    result = result.AddDays(1);
                    break;
            }

            return result;
        }

        /// <summary>
        /// Assigns students to rooms for the given course.
        /// </summary>
        /// <param name="course">
        /// The course to schedule for.
        /// </param>
        /// <param name="courseStudents">
        /// The students to try and assign.
        /// </param>
        /// <param name="availableFaculty">
        /// Faculty members that are available for scheduling for this course.
        /// </param>
        /// <param name="labRoom">
        /// If we're scheduling for labs.
        /// </param>
        /// <param name="classRoom">
        /// If we're scheduling for class rooms.
        /// </param>
        /// <param name="instructRoom">
        /// If we're scheduling for instructional rooms.
        /// </param>
        /// <returns>
        /// A list of students that were unable to be scheduled.
        /// </returns>
        private List<Student> AssignStudents(Course course, List<Student> courseStudents, List<Faculty> availableFaculty, bool labRoom, bool classRoom, bool instructRoom, bool library)
        {
            int currentFacultyIndex = 0;
            int requiredHours = course.Requirement.LabHr;

            while (requiredHours > 0)
            {
                int assignedHours = requiredHours > 2 ? 2 : requiredHours;

                // This is the confusing part, we can assign students across different hours if there's too many
                // Now here's the confusing part, we locate a teacher and assign consecutive slots until there's no students
                while (courseStudents.Count > 0)
                {
                    // We can assign either 1 or 2 hour slots, so first see if we can find a 2 hour available lab
                    List<TimeSlot> availability = RequestAvailableRooms(course, labRoom, classRoom, instructRoom, library);

                    // Find the largest available room here
                    TimeSlot largest = FindLargestRoom(availability, courseStudents.Count, assignedHours);

                    // If there is no available slot, we're slightly screwed and will have to forget about scheduling the rest now
                    if (largest == null)
                    {
                        foreach (Student student in courseStudents)
                        {
                            if (!UnscheduledCourses.ContainsKey(student.StudentID))
                                UnscheduledCourses[student.StudentID] = new List<Course>();

                            UnscheduledCourses[student.StudentID].Add(course);
                        }

                        UpdateProgress(string.Format("Failed to schedule {0} students for course {1}", courseStudents.Count, course.Names));
                        return courseStudents;
                    }

                    // Our endDate is the start plus assigned hours
                    DateTime endDate = largest.Start.AddHours(assignedHours);

                    // We've got some slot available, ram everyone into it and keep going
                    Faculty teachingFaculty = availableFaculty[currentFacultyIndex++ % availableFaculty.Count];

                    Schedule newSchedule = new Schedule()
                    {
                        StartTime = largest.Start,
                        EndTime = endDate,
                        CourseID = course.CourseID,
                        RoomID = largest.TargetRoom.RoomID,
                    };
                    CurrentSchedules.Add(newSchedule);
                    Program.Database.Schedules.Add(newSchedule);

                    // Add the teaching faculty time first
                    FacultySchedule facultySchedule = new FacultySchedule()
                    {
                        StartTime = largest.Start,
                        FacultyID = teachingFaculty.FacultyID,
                        RoomID = largest.TargetRoom.RoomID,
                        Schedule = newSchedule,
                    };

                    Program.Database.FacultySchedules.Add(facultySchedule);

                    // Now assign all the students
                    int removedCount = largest.TargetRoom.Capacity > courseStudents.Count ? courseStudents.Count : largest.TargetRoom.Capacity;
                    List<Student> assignedStudents = courseStudents.GetRange(0, removedCount);
                    courseStudents.RemoveRange(0, removedCount);

                    foreach (Student student in assignedStudents)
                    {
                        StudentSchedule studentSchedule = new StudentSchedule()
                        {
                            StudentID = student.StudentID,
                            StartTime = largest.Start,
                            Schedule = newSchedule,
                        };

                        Program.Database.StudentSchedules.Add(studentSchedule);
                    }

                }

                requiredHours -= assignedHours;
            }

            if (courseStudents.Count != 0)
                Console.WriteLine("DUH");

            return courseStudents;
        }

        private void ButtonScheduleUpdate_Click(object sender, EventArgs e)
        {
            LabelScheduleUpdate.Text = "Updating ... Please wait.";
            Refresh();

            if (UnscheduledStudents == null)
                UnscheduledStudents = new List<Student>();
            if (CurrentSchedules == null)
                CurrentSchedules = new List<Schedule>();
            if (UnscheduledCourses == null)
                UnscheduledCourses = new Dictionary<int, List<Course>>();

            UnscheduledStudents.Clear();
            CurrentSchedules.Clear();

            #region Nuking
            UpdateProgress("Deleting old schedules ...");

            // Blow out the schedule table
            List<Schedule> schedules = Program.Database.Schedules.ToList();
            foreach (Schedule schedule in schedules)
                Program.Database.Schedules.Remove(schedule);

            // Blow out the schedule table
            List<FacultySchedule> facultySchedules = Program.Database.FacultySchedules.ToList();
            foreach (FacultySchedule schedule in facultySchedules)
                Program.Database.FacultySchedules.Remove(schedule);

            // Blow out the schedule table
            List<StudentSchedule> studentSchedules = Program.Database.StudentSchedules.ToList();
            foreach (StudentSchedule studentSchedule in studentSchedules)
                Program.Database.StudentSchedules.Remove(studentSchedule);

            Program.Database.SaveChanges();
            #endregion

            InitializeHardcodedData();

            UpdateProgress("Beginning schedule process ...");

            // Blow through each major so we can find all the courses
            foreach (Major major in Program.Database.Majors)
            {
                UpdateProgress(string.Format("Starting schedule for major {0}", major.Major1));

                // Unfortunately we're doing the student assignment here as well, so we assign according to the hardcoded student assignments
                List<Student> students = (from student in Program.Database.Students
                                          where (from majorLink in student.StudentMajors
                                                 where
                                                 majorLink.MajorID == major.MajorID
                                                 select majorLink).FirstOrDefault() != null
                                          select student).ToList();

                List<Faculty> availableFaculty = (from facultyLink in Program.Database.FacultyMajors
                                                  where facultyLink.MajorID == major.MajorID
                                                  select facultyLink.Faculty).ToList();

                // Now we blow through each course to perform the assignments
                foreach (Course course in major.Courses)
                {
                    UpdateProgress(string.Format("Starting schedule for course {0}", course.Names));

                    // According to our hardcoded requirements, get a list of students;
                    int courseStudentsCount = StudentCounts[course.CourseID] > students.Count ? students.Count : StudentCounts[course.CourseID];

                    if (course.Requirement.LabHr != 0)
                        UnscheduledStudents.AddRange(AssignStudents(course, students.GetRange(0, courseStudentsCount), availableFaculty, true, false, false, false));
                    if (course.Requirement.InstructHr != 0)
                        UnscheduledStudents.AddRange(AssignStudents(course, students.GetRange(0, courseStudentsCount), availableFaculty, false, false, true, false));
                    if (course.Requirement.LibHr != 0)
                        UnscheduledStudents.AddRange(AssignStudents(course, students.GetRange(0, courseStudentsCount), availableFaculty, false, false, false, true));
                    if (course.Requirement.ClassHr != 0)
                        UnscheduledStudents.AddRange(AssignStudents(course, students.GetRange(0, courseStudentsCount), availableFaculty, false, true, false, false));

                    students.RemoveRange(0, courseStudentsCount);
                    UpdateProgress(string.Format("Finished schedule for course {0}", course.Names));
                }

                UpdateProgress(string.Format("Finished schedule for major {0}", major.Major1));
            }

            UpdateProgress("Schedule complete!");

            // Update the list box
            UnscheduledStudents = UnscheduledStudents.Distinct().ToList();

            ListBoxUnscheduledStudents.Items.Clear();
            foreach (Student student in UnscheduledStudents)
            {
                string unscheduledCourses = "";
                if (UnscheduledCourses.ContainsKey(student.StudentID))
                    foreach (Course course in UnscheduledCourses[student.StudentID])
                        unscheduledCourses += course.CourseID + ", ";

                ListBoxUnscheduledStudents.Items.Add(string.Format("{0} {1} {2}", student.FName, student.LName, unscheduledCourses));
            }

            Program.Database.SaveChanges();
            LabelScheduleUpdate.Text = "Schedule update finished.";

            if (UnscheduledStudents.Count != 0)
            {
                LabelUnscheduledStudents.Text = "Failed to schedule " + UnscheduledStudents.Count + " students.";
                MessageBox.Show("Failed to assign some students and faculty to the schedule due to no time slot availability. Please check the listings for details.", "Error");
            }
            else
                LabelUnscheduledStudents.Text = "";
        }

        private void ListBoxUnscheduledStudents_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ListBoxUnscheduledStudents.SelectedIndex == -1 || ListBoxUnscheduledStudents.SelectedIndex >= UnscheduledStudents.Count)
                return;

            Student target = UnscheduledStudents[ListBoxUnscheduledStudents.SelectedIndex];

            FormStudentDetails details = new FormStudentDetails(target);
            details.ShowDialog();
        }
    }
}
