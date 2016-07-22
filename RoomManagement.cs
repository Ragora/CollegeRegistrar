using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrarConsole
{
    public partial class FormMain
    {
        private List<Room> QueriedRooms;

        /// <summary>
        /// Search for rooms that contain a given substring in their
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private List<Room> SearchRooms(string number, List<Course> filter)
        {
            number = number.ToLower();

            var query = from room in Program.Database.Rooms
                        where room.RoomID.Contains(number)
                       select room;

            List<Room> results = new List<Room>();
            foreach (Room room in query)
            {
                if (room.ResourceID == null)
                {
                    results.Add(room);
                    continue;
                }

                // We should match each course
                bool goodCourse = true;
                foreach (Course course in filter)
                    if (!(room.Resource.Program && course.Requirement.Program) && !(room.Resource.Cad && course.Requirement.Cad) && !(room.Resource.Gaming && course.Requirement.Gaming) && !(room.Resource.Multi && course.Requirement.Multi)
                        && !(!course.Requirement.Program && !course.Requirement.Cad && !course.Requirement.Gaming && !course.Requirement.Multi))
                    {
                        goodCourse = false;
                        break;
                    }

                if (goodCourse)
                    results.Add(room);
            }

            return results;
        }

        /// <summary>
        /// Called when the update search button on the room tab has been clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUpdateRoomSearch_Click(object sender, EventArgs e)
        {
            ListBoxRoomResults.Items.Clear();

            List<string> courseIDs = GetSelectedNames(CheckedListBoxRoomFilters);

            List<Course> filter = (from course in Program.Database.Courses
                                  where courseIDs.Contains(course.CourseID)
                                  select course).ToList();

            QueriedRooms = SearchRooms(TextBoxRoomNumberSearch.Text, filter);
            foreach (Room room in QueriedRooms)
                ListBoxRoomResults.Items.Add("Room " + room.RoomID);
        }

        /// <summary>
        /// Called when the user double clicks on a room search result, indicating a result selection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxRoomResults_ResultDoubleClicked(object sender, MouseEventArgs e)
        {
            if (QueriedRooms == null || ListBoxRoomResults.SelectedIndex == -1)
                return;

            // Find the selected room
            Room room = QueriedRooms[ListBoxRoomResults.SelectedIndex];

            FormRoomDetails details = new FormRoomDetails(room);
            details.ShowDialog();
        }

        /// <summary>
        /// Called when the user clicks the clear search button on the room management tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRoomClearSearch_Click(object sender, EventArgs e)
        {
            TextBoxRoomNumberSearch.Clear();
            ListBoxRoomResults.Items.Clear();

            foreach (int index in CheckedListBoxRoomFilters.CheckedIndices)
                CheckedListBoxRoomFilters.SetItemChecked(index, false);
        }

        private void GroupBoxRoomSearch_VisibleChanged(object sender, EventArgs e)
        {
            CheckedListBoxRoomFilters.Items.Clear();

            List<Course> courses = Program.Database.Courses.ToList();

            foreach (Course course in Program.Database.Courses)
                CheckedListBoxRoomFilters.Items.Add(course.CourseID);
        }

        /// <summary>
        /// Called when the user clicks the clear search button in the room management tab to clear
        /// typed in information for a new room.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClearNewRoom_Click(object sender, EventArgs e)
        {
            NumericUpDownNewRoomCapacity.Value = 0;
            TextBoxNewRoomNumber.Clear();

            CheckBoxNewRoomClassroom.Checked = false;
            CheckBoxNewRoomLab.Checked = false;
            CheckBoxNewRoomInstructional.Checked = false;

            foreach (int index in CheckedListBoxNewRoomResources.CheckedIndices)
                CheckedListBoxNewRoomResources.SetItemChecked(index, false);
        }

        /// <summary>
        /// Called when the user clicks the create button on the room management tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCreateNewRoom_Click(object sender, EventArgs e)
        {
            TextBoxNewRoomNumber.Text = TextBoxNewRoomNumber.Text.Trim();

            string roomNumber = TextBoxNewRoomNumber.Text;
            if (roomNumber.Length == 0)
            {
                MessageBox.Show("Please assign the room a room number.", "Error");
                return;
            }

            // How many people can fit in here?
            int headCount = (int)NumericUpDownNewRoomCapacity.Value;
            if (headCount == 0)
            {
                MessageBox.Show("Can't have a room with support for zero people.", "Error");
                return;
            }

            // Create a resource entry
            Resource resource = new Resource()
            {
                Program = CheckedListBoxNewRoomResources.CheckedItems.Contains("Programming Software"),
                Cad = CheckedListBoxNewRoomResources.CheckedItems.Contains("CAD Software"),
                Multi = CheckedListBoxNewRoomResources.CheckedItems.Contains("Multi-Media Software"),
                Gaming = CheckedListBoxNewRoomResources.CheckedItems.Contains("Gaming Software"),
                Smartboard = CheckedListBoxNewRoomResources.CheckedItems.Contains("Smart Board"),
                PodWithProj = CheckedListBoxNewRoomResources.CheckedItems.Contains("Projector Pod"),

                Classroom = CheckBoxNewRoomClassroom.Checked,
                Instruct = CheckBoxNewRoomInstructional.Checked,
                Lab = CheckBoxNewRoomLab.Checked,
            };

            // See if there's a resource with these specifications already
            foreach (Resource existing in Program.Database.Resources)
                if (existing.Program == resource.Program && existing.Cad == resource.Cad && existing.Multi == resource.Multi && 
                    existing.Gaming == resource.Gaming && existing.Smartboard == resource.Smartboard && existing.PodWithProj == resource.PodWithProj &&
                    existing.Classroom == resource.Classroom && existing.Instruct == resource.Instruct && existing.Lab == resource.Lab)
                {
                    resource = existing;
                    break;
                }

            // Attempt the database creation
            Room newRoom = new Room()
            {
                RoomID = roomNumber,
                ResourceID = resource.ResourceID,
                Capacity = (int)NumericUpDownNewRoomCapacity.Value,
            };

            Program.Database.Resources.Add(resource);
            Program.Database.Rooms.Add(newRoom);
            Program.Database.SaveChanges();

            MessageBox.Show("Successfully created the new room.", "Success");

            // Clear the existing information
            ButtonClearNewRoom_Click(sender, e);
        }
    }
}
