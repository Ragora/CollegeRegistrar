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
    /// <summary>
    /// Form used to display and edit faculty information.
    /// </summary>
    public partial class FormFacultyDetails : Form
    {
        /// <summary>
        /// The target faculty member.
        /// </summary>
        private Faculty Target;

        public FormFacultyDetails(Faculty target)
        {
            Target = target;

            InitializeComponent();
        }

        private void FormFacultyDetails_Load(object sender, EventArgs e)
        {
            TextBoxFacultyFirstName.Text = Target.FName;
            TextBoxFacultyLastName.Text = Target.LName;

            TextBoxFacultyPrimaryEMail.Text = Target.Contact.PrimaryEmail;
            TextBoxFacultyPrimaryNumber.Text = Target.Contact.PrimaryNumber;
            TextBoxFacultySecondaryEMail.Text = Target.Contact.SecondaryEmail;
            TextBoxFacultySecondaryNumber.Text = Target.Contact.SecondaryNumber;

            // Populate the combo box major
            CheckedListBoxFacultyMajors.Items.Clear();
            foreach (Major major in Program.Database.Majors)
            {
                if (major.MajorID == "GenEd")
                    continue;

                CheckedListBoxFacultyMajors.Items.Add(major.Major1);
            }

            // Now figure out which ones should be checked
            var majorLinks = from link in Program.Database.FacultyMajors
                             where link.FacultyID == Target.FacultyID select link;

            foreach (FacultyMajor link in majorLinks)
                CheckedListBoxFacultyMajors.SetItemChecked(CheckedListBoxFacultyMajors.Items.IndexOf(link.Major.Major1), true);
        }

        private void ButtonCancelFaculty_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonDeleteFaculty_Click(object sender, EventArgs e)
        {
            Program.Database.Faculties.Remove(Target);
            Program.Database.Contacts.Remove(Program.Database.Contacts.Find(Target.ContactID));
            Program.Database.SaveChanges();

            Close();
        }

        private void ButtonSaveFaculty_Click(object sender, EventArgs e)
        {
            Target.FName = TextBoxFacultyFirstName.Text;
            Target.LName = TextBoxFacultyLastName.Text;

            // Verify the emails and numbers
            string result;

            if (!StringHelpers.FormatEMail(TextBoxFacultyPrimaryEMail.Text, out result))
            {
                MessageBox.Show("Please ensure that the primary E-Mail is valid before saving that faculty member.", "Error");
                return;
            }

            if (!StringHelpers.FormatPhoneNumber(TextBoxFacultyPrimaryNumber.Text, out result))
            {
                MessageBox.Show("Please ensure that the primary phone number is valid before saving that faculty member.", "Error");
                return;
            }

            // Verify secondaries if they're specified
            if (TextBoxFacultySecondaryEMail.Text.Length != 0 && !StringHelpers.FormatEMail(TextBoxFacultySecondaryEMail.Text, out result))
            {
                MessageBox.Show("Please ensure that the secondary E-Mail is valid before saving that faculty member.", "Error");
                return;
            }

            if (TextBoxFacultySecondaryNumber.Text.Length != 0 && !StringHelpers.FormatPhoneNumber(TextBoxFacultySecondaryNumber.Text, out result))
            {
                MessageBox.Show("Please ensure that the secondary phone number is valid before saving that faculty member.", "Error");
                return;
            }

            if (CheckedListBoxFacultyMajors.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please specify the major that this faculty member will be instructing.", "Error");
                return;
            }

            Target.Contact.PrimaryEmail = TextBoxFacultyPrimaryEMail.Text;
            Target.Contact.PrimaryNumber = TextBoxFacultyPrimaryNumber.Text;

            Target.Contact.SecondaryEmail = TextBoxFacultySecondaryEMail.Text.Length == 0 ? null : TextBoxFacultySecondaryEMail.Text;
            Target.Contact.SecondaryNumber = TextBoxFacultySecondaryNumber.Text.Length == 0 ? null : TextBoxFacultySecondaryNumber.Text;

            // Find whatever majors they are going to be teaching
            List<string> checkedMajors = FormMain.GetSelectedNames(CheckedListBoxFacultyMajors);
            foreach (string major in checkedMajors)
            {
                string majorID = (from search in Program.Database.Majors
                                  where search.Major1 == major
                                  select search).First().MajorID;

                FacultyMajor newMajor = new FacultyMajor()
                {
                    MajorID = majorID,
                    FacultyID = Target.FacultyID,
                };

                Target.FacultyMajors.Add(newMajor);
            }

            Program.Database.SaveChanges();
            Close();
        }

        private void TextBoxFacultyPrimaryNumber_Leave(object sender, EventArgs e)
        {
            string result;
            StringHelpers.FormatPhoneNumber(TextBoxFacultyPrimaryNumber.Text, out result);
            TextBoxFacultyPrimaryNumber.Text = result;
        }

        private void TextBoxFacultySecondaryNumber_Leave(object sender, EventArgs e)
        {
            TextBoxFacultySecondaryNumber.Text = TextBoxFacultySecondaryNumber.Text.Trim();

            if (TextBoxFacultySecondaryNumber.Text.Length == 0)
                return;

            string result;
            StringHelpers.FormatPhoneNumber(TextBoxFacultySecondaryNumber.Text, out result);
            TextBoxFacultySecondaryNumber.Text = result;
        }

        private void TextBoxFacultyEMail_Leave(object sender, EventArgs e)
        {
            TextBox target = (TextBox)sender;

            target.Text = TextBoxFacultySecondaryEMail.Text.Trim();

            if (target.Text.Length == 0)
                return;

            string result;
            StringHelpers.FormatEMail(target.Text, out result);
            target.Text = result;
        }
    }
}
