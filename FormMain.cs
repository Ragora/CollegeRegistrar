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
    public partial class FormMain : Form
    {
        /// <summary>
        /// Constructor called when the application first starts on the main form.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            Program.Database = new UniversityBoulevardEntities();

            CheckedListBoxNewRoomResources.Items.Add("Gaming Software");
            CheckedListBoxNewRoomResources.Items.Add("CAD Software");
            CheckedListBoxNewRoomResources.Items.Add("Multi-Media Software");
            CheckedListBoxNewRoomResources.Items.Add("Programming Software");
            CheckedListBoxNewRoomResources.Items.Add("Smart Board");
            CheckedListBoxNewRoomResources.Items.Add("Projector Pod");
        }

        private List<Course> SearchCourses(string id)
        {
            var query = from course in Program.Database.Courses
                        where course.CourseID.Contains(id)
                        select course;

            return query.ToList();
        }

        public static List<string> GetSelectedNames(CheckedListBox box)
        {
            List<string> result = new List<string>();
            foreach (var entry in box.CheckedItems)
                result.Add((string)entry);

            return result;
        }

        /// <summary>
        /// Returns a list of all known majors currently in the database.
        /// </summary>
        /// <returns>
        /// A list of majors in the database.
        /// </returns>
        private List<Major> GetMajors()
        {
            var query = from major in Program.Database.Majors
                      select major;

            return query.ToList();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}
