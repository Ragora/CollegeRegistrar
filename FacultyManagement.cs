using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace RegistrarConsole
{
    /// <summary>
    /// A class representing the main form of the registar application.
    /// </summary>
    public partial class FormMain
    {
        /// <summary>
        /// A list of faculty we have currently queried.
        /// </summary>
        private List<Faculty> QueriedFaculty;

        /// <summary>
        /// Called when the user presses the clear search button on the faculty management tab to
        /// clear search information and results.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonFacultyClearSearch_Click(object sender, EventArgs e)
        {
            TextBoxFacultyFirstName.Clear();
            TextBoxFacultyLastName.Clear();
            ListBoxFacultyResults.Items.Clear();
            
            foreach (int index in CheckedListBoxFacultyFilters.CheckedIndices)
                CheckedListBoxFacultyFilters.SetItemChecked(index, false);
        }

        private void TextBoxNewFacultyPhone_Leave(object sender, EventArgs e)
        {
            string result;
            StringHelpers.FormatPhoneNumber(TextBoxNewFacultyPhone.Text, out result);
            TextBoxNewFacultyPhone.Text = result;
        }

        private void TextBoxNewFacultyEmail_Leave(object sender, EventArgs e)
        {
            string result;
            StringHelpers.FormatEMail(TextBoxNewFacultyEmail.Text, out result);
            TextBoxNewFacultyEmail.Text = result;
        }

        Faculty CreateFaculty(string fname, string lname, string pphone, string pemail, string sphone = null, string semail = null)
        {
            Contact contact = new Contact()
            {
                PrimaryEmail = pemail,
                PrimaryNumber = pphone,
                SecondaryEmail = semail,
                SecondaryNumber = sphone,
            };
            Program.Database.Contacts.Add(contact);

            // Find whatever majors they are going to be teaching
            List<string> selectedMajors = GetSelectedNames(CheckedListBoxNewFacultyMajors);

            var majors = from search in Program.Database.Majors
                         where selectedMajors.Contains(search.Major1)
                         select search;

            Faculty result = new Faculty()
            {
                ContactID = contact.ContactID,
                FName = fname,
                LName = lname,
                WorkStatus = "F",
            };

            // Create the major relationships
            foreach (Major major in majors)
            {
                FacultyMajor relationship = new FacultyMajor()
                {
                    FacultyID = result.FacultyID,
                    MajorID = major.MajorID,
                };

                Program.Database.FacultyMajors.Add(relationship);
            }

            Program.Database.Faculties.Add(result);
            Program.Database.SaveChanges();

            return result;
        }

        private void ButtonClearNewFaculty_Click(object sender, EventArgs e)
        {
            TextBoxNewFacultyFirstName.Clear();
            TextBoxNewFacultyLastName.Clear();

            TextBoxNewFacultyPhone.Clear();
            TextBoxNewFacultyEmail.Clear();
        }

        private void ButtonCreateNewFaculty_Click(object sender, EventArgs e)
        {
            if (TextBoxNewFacultyFirstName.Text.Length == 0)
            {
                MessageBox.Show("Please specify a first name for this new faculty member.", "Error");
                return;
            }
            else if (TextBoxNewFacultyLastName.Text.Length == 0)
            {
                MessageBox.Show("Please specify a last name for this new faculty member.", "Error");
                return;
            }
            else if (TextBoxNewFacultyPhone.Text.Length == 0)
            {
                MessageBox.Show("Please specify a phone number for this new faculty member.", "Error");
                return;
            }
            else if (TextBoxNewFacultyEmail.Text.Length == 0)
            {
                MessageBox.Show("Please specify a E-Mail name for this new faculty member.", "Error");
                return;
            }
            else if (CheckedListBoxNewFacultyMajors.SelectedItems.Count == 0)
            {
               MessageBox.Show("Please specify the major that this new faculty member will be instructing for.", "Error");
                return;
            }

            Faculty member = CreateFaculty(TextBoxNewFacultyFirstName.Text, TextBoxNewFacultyLastName.Text,
                TextBoxNewFacultyPhone.Text, TextBoxNewFacultyEmail.Text);
            MessageBox.Show("Successfully added new faculty member.", "Success");

            ButtonClearNewFaculty_Click(sender, e);
        }

        /// <summary>
        /// Called when the faculty management tab is shown to the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupBoxAddFaculty_VisibleChanged(object sender, EventArgs e)
        {
            CheckedListBoxFacultyFilters.Items.Clear();
            CheckedListBoxNewFacultyMajors.Items.Clear();

            foreach (Major major in GetMajors())
            {
                CheckedListBoxNewFacultyMajors.Items.Add(major.Major1);    
                CheckedListBoxFacultyFilters.Items.Add(major.Major1);
            }
        }

        /// <summary>
        /// Called when the user presses the update search button on the faculty management tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonFacultyUpdateSearch_Click(object sender, EventArgs e)
        {
            ListBoxFacultyResults.Items.Clear();

            List<string> majorFilter = new List<string>();
            if (CheckedListBoxFacultyFilters.CheckedItems.Count != 0)
                majorFilter = GetSelectedNames(CheckedListBoxFacultyFilters);

            QueriedFaculty = SearchFaculty(TextBoxFacultyFirstName.Text, TextBoxFacultyLastName.Text, majorFilter);

            foreach (Faculty member in QueriedFaculty)
                ListBoxFacultyResults.Items.Add(string.Format("{0} {1}", member.FName, member.LName));
        }
        
        /// <summary>
        /// Queries the database for a faculty member whose first and last name contains the given strings.
        /// </summary>
        /// <param name="first">
        /// The substring to search for in the first name. If null, this is simply ignored.
        /// </param>
        /// <param name="last">
        /// The substring to search for in the last name. If null, this is simply ignored.
        /// </param>
        /// <returns>
        /// A list of faculty members matching the search criteria.
        /// </returns>
        private List<Faculty> SearchFaculty(string first, string last, List<string> filter)
        {
            first = first != null ? first.ToLower() : "";
            last = last != null ? last.ToLower() : "";

            List<Faculty> result = new List<Faculty>();

            var nameQuery = from faculty in Program.Database.Faculties
                            where faculty.FName.Contains(first) &&
                            faculty.LName.Contains(last)  // && (filter.Contains(faculty.MajorID) || filter.Contains(faculty.Major.Major1))
                            select faculty;

            if (filter.Count == 0)
                return nameQuery.ToList();

            foreach (Faculty faculty in nameQuery)
                foreach (FacultyMajor major in faculty.FacultyMajors)
                    if (filter.Contains(major.Major.Major1) || filter.Contains(major.Major.MajorID))
                    {
                        result.Add(faculty);
                        break;
                    }

            return result;
        }

        private void ListBoxFacultyResults_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (QueriedFaculty == null || QueriedFaculty.Count < ListBoxFacultyResults.SelectedIndex || ListBoxFacultyResults.SelectedIndex < 0)
                return;

            Faculty target = QueriedFaculty[ListBoxFacultyResults.SelectedIndex];

            FormFacultyDetails details = new FormFacultyDetails(target);
            details.ShowDialog();

            ButtonFacultyUpdateSearch_Click(sender, e);
        }
    }
}
