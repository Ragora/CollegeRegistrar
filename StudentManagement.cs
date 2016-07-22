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
        private List<Student> QueriedStudents;

        /// <summary>
        /// Submits a search query for a student whose name contains the given first and last names.
        /// </summary>
        /// <param name="first">
        /// The first name substring to search by. If null, the first name is ignored in the search.
        /// </param>
        /// <param name="last">
        /// The last name substring to search by. If null, the last name is ignored in the search.
        /// </param>
        /// <returns>
        /// A list of students matching the search parameters.
        /// </returns>
        private List<Student> SearchStudents(string first, string last, List<string> majors)
        {
            first = first != null ? first.ToLower() : "";
            last = last != null ? last.ToLower() : "";

            // Check to see if any of the student majors are here
            var query = from student in Program.Database.Students
                        where student.FName.Contains(first) &&
                        student.LName.Contains(last)
                        select student;

            List<Student> result = new List<Student>();

            if (majors.Count == 0)
                return query.ToList();

            foreach (Student student in query)
                foreach (StudentMajor major in student.StudentMajors)
                    if (majors.Contains(major.Major.Major1) || majors.Contains(major.Major.MajorID))
                    {
                        result.Add(student);
                        break;
                    }

            return result;
        }

        /// <summary>
        /// Called when the user presses the update button to update the search queries in the student
        /// management tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStudentUpdateSearch_Click(object sender, EventArgs e)
        {
            ListBoxStudentResults.Items.Clear();

            List<string> majorFilter = new List<string>();
            if (CheckedListBoxStudentFilters.CheckedItems.Count != 0)
                majorFilter = GetSelectedNames(CheckedListBoxStudentFilters);

            QueriedStudents = SearchStudents(TextBoxStudentFirstName.Text, TextBoxStudentLastName.Text, majorFilter);
            foreach (Student student in QueriedStudents)
                ListBoxStudentResults.Items.Add(string.Format("{0} {1}", student.FName, student.LName));
        }

        /// <summary>
        /// Button called when the user presses the clear search button to clear current search
        /// query information and results in the student management tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStudentClearSearch_Click(object sender, EventArgs e)
        {
            ListBoxStudentResults.Items.Clear();
            TextBoxStudentFirstName.Clear();
            TextBoxStudentLastName.Clear();

            foreach (int index in CheckedListBoxStudentFilters.CheckedIndices)
                CheckedListBoxStudentFilters.SetItemChecked(index, false);
        }
        
        /// <summary>
        /// Called when the student management tab is opened. At least, in this case it is essentially
        /// that anyway.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupBoxStudentSearch_VisibleChanged(object sender, EventArgs e)
        {
            List<Major> majors = GetMajors();
            CheckedListBoxStudentFilters.Items.Clear();

            foreach (Major major in majors)
                CheckedListBoxStudentFilters.Items.Add(major.Major1);
        }

        private void ListBoxStudentResults_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (QueriedStudents == null || ListBoxStudentResults.SelectedIndex < 0 || ListBoxStudentResults.SelectedIndex >= QueriedStudents.Count())
                return;

            Student student = QueriedStudents[ListBoxStudentResults.SelectedIndex];

            FormStudentDetails details = new FormStudentDetails(student);
            details.ShowDialog();

            ButtonStudentUpdateSearch_Click(sender, e);
        }
    }
}
