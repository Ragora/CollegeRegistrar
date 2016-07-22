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
    /// The form used for displaying and editing of student details.
    /// </summary>
    public partial class FormStudentDetails : Form
    {
        /// <summary>
        /// The target student we are working with.
        /// </summary>
        private Student Target;

        public FormStudentDetails(Student target)
        {
            InitializeComponent();

            Target = target;
        }

        private void FormStudentDetails_Load(object sender, EventArgs e)
        {
            TextBoxStudentFirstName.Text = Target.FName;
            TextBoxStudentLastName.Text = Target.LName;
            TextBoxStudentPrimaryEMail.Text = Target.Contact.PrimaryEmail;
            TextBoxStudentPrimaryNumber.Text = Target.Contact.PrimaryNumber;

            // Populate the checked majors
            CheckedListBoxMajor.Items.Clear();

            // Populate the combo box major
            foreach (Major major in Program.Database.Majors)
            {
                if (major.MajorID == "GenEd")
                    continue;

                CheckedListBoxMajor.Items.Add(major.Major1);
            }

            // Check the majors
            foreach (StudentMajor major in Target.StudentMajors)
            {
                if (major.MajorID == "GenEd")
                    continue;

                int listIndex = CheckedListBoxMajor.Items.IndexOf(major.Major.Major1);
                CheckedListBoxMajor.SetItemChecked(listIndex, true);
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            Close();

            UserInfo login = (from info in Program.Database.UserInfoes
                              where info.StudentID == Target.StudentID
                              select info).SingleOrDefault();

            StudentSchedule scheduleLink = (from linkobj in Program.Database.StudentSchedules
                                            where linkobj.StudentID == Target.StudentID
                                            select linkobj).SingleOrDefault();

            if (Target.ScheduleID != null)
            {
                Program.Database.Schedules.Remove(Program.Database.Schedules.Find(Target.ScheduleID));
                Program.Database.StudentSchedules.Remove(scheduleLink);
            }

            if (login != null)
                Program.Database.UserInfoes.Remove(login);

            Program.Database.Addresses.Remove(Program.Database.Addresses.Find(Target.AddressID));
            Program.Database.Contacts.Remove(Target.Contact);
            Program.Database.Students.Remove(Target);

            Program.Database.SaveChanges();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {

            Target.FName = TextBoxStudentFirstName.Text;
            Target.LName = TextBoxStudentLastName.Text;

            // Verify the emails and numbers
            string result;

            if (!StringHelpers.FormatEMail(TextBoxStudentPrimaryEMail.Text, out result))
            {
                MessageBox.Show("Please ensure that the primary E-Mail is valid before saving that student.", "Error");
                return;
            }

            if (!StringHelpers.FormatPhoneNumber(TextBoxStudentPrimaryNumber.Text, out result))
            {
                MessageBox.Show("Please ensure that the primary phone number is valid before saving that student.", "Error");
                return;
            }

            // Verify secondaries if they're specified
            if (TextBoxStudentSecondaryEMail.Text.Length != 0 && !StringHelpers.FormatEMail(TextBoxStudentSecondaryEMail.Text, out result))
            {
                MessageBox.Show("Please ensure that the secondary E-Mail is valid before saving that student.", "Error");
                return;
            }

            if (TextBoxStudentSecondaryNumber.Text.Length != 0 && !StringHelpers.FormatPhoneNumber(TextBoxStudentSecondaryNumber.Text, out result))
            {
                MessageBox.Show("Please ensure that the secondary phone number is valid before saving that student.", "Error");
                return;
            }

            if (CheckedListBoxMajor.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please specify the major that this student will be attending.", "Error");
                return;
            }

            // Clear out the majors
            Target.StudentMajors.Clear();

            StudentMajor genED = new StudentMajor()
            {
                MajorID = "GenEd",
                StudentID = Target.StudentID,
            };
            Program.Database.StudentMajors.Add(genED);

            List<string> checkedMajors = FormMain.GetSelectedNames(CheckedListBoxMajor);
            foreach (string major in checkedMajors)
            {
                string majorID = (from search in Program.Database.Majors
                                  where search.Major1 == major
                                  select search).First().MajorID;

                StudentMajor newMajor = new StudentMajor()
                {
                    MajorID = majorID,
                    StudentID = Target.StudentID,
                };

                Target.StudentMajors.Add(newMajor);
            }

            Program.Database.SaveChanges();
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TextBoxStudentSecondaryEMail_Leave(object sender, EventArgs e)
        {
            TextBoxStudentSecondaryEMail.Text = TextBoxStudentSecondaryEMail.Text.Trim();

            if (TextBoxStudentSecondaryEMail.Text.Length == 0)
                return;

            string result;
            StringHelpers.FormatPhoneNumber(TextBoxStudentSecondaryEMail.Text, out result);
            TextBoxStudentSecondaryEMail.Text = result;
        }

        private void TextBoxStudentSecondaryNumber_Leave(object sender, EventArgs e)
        {
            TextBoxStudentSecondaryNumber.Text = TextBoxStudentSecondaryNumber.Text.Trim();

            if (TextBoxStudentSecondaryNumber.Text.Length == 0)
                return;

            string result;
            StringHelpers.FormatPhoneNumber(TextBoxStudentSecondaryNumber.Text, out result);
            TextBoxStudentSecondaryNumber.Text = result;
        }
    }
}
