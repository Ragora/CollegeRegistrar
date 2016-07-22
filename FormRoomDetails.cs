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
    public partial class FormRoomDetails : Form
    {
        private Room Target;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="roomNum"></param>
        public FormRoomDetails(Room target)
        {
            InitializeComponent();

            Target = target;

            CheckedListBoxRoomResources.Items.Clear();
            CheckedListBoxRoomResources.Items.Add("Gaming Software");
            CheckedListBoxRoomResources.Items.Add("CAD Software");
            CheckedListBoxRoomResources.Items.Add("Multi-Media Software");
            CheckedListBoxRoomResources.Items.Add("Programming Software");
            CheckedListBoxRoomResources.Items.Add("Smart Board");
            CheckedListBoxRoomResources.Items.Add("Projector Pod");

            // Check the appropriate items
            if (Target.Resource != null)
            {
                CheckedListBoxRoomResources.SetItemChecked(0, Target.Resource.Gaming);
                CheckedListBoxRoomResources.SetItemChecked(1, Target.Resource.Cad);
                CheckedListBoxRoomResources.SetItemChecked(2, Target.Resource.Multi);
                CheckedListBoxRoomResources.SetItemChecked(3, Target.Resource.Program);
                CheckedListBoxRoomResources.SetItemChecked(4, Target.Resource.Smartboard);
                CheckedListBoxRoomResources.SetItemChecked(5, Target.Resource.PodWithProj);

                CheckBoxRoomClassroom.Checked = Target.Resource.Classroom;
                CheckBoxRoomLab.Checked = Target.Resource.Lab;
                CheckBoxRoomInstructional.Checked = Target.Resource.Instruct;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoomDetails_Load(object sender, EventArgs e)
        {
            GroupBoxRoomDetails.Text += " " + Target.RoomID;
        }

        /// <summary>
        /// Called when the user presses the cancel button to simply exit without changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancelRoom_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Called when the user presses the save button to save modified information to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSaveRoom_Click(object sender, EventArgs e)
        {
            Close();

            Resource resource = new Resource()
            {
                Program = CheckedListBoxRoomResources.CheckedItems.Contains("Programming Software"),
                Cad = CheckedListBoxRoomResources.CheckedItems.Contains("CAD Software"),
                Multi = CheckedListBoxRoomResources.CheckedItems.Contains("Multi-Media Software"),
                Gaming = CheckedListBoxRoomResources.CheckedItems.Contains("Gaming Software"),
                Smartboard = CheckedListBoxRoomResources.CheckedItems.Contains("Smart Board"),
                PodWithProj = CheckedListBoxRoomResources.CheckedItems.Contains("Projector Pod"),

                Classroom = CheckBoxRoomClassroom.Checked,
                Instruct = CheckBoxRoomInstructional.Checked,
                Lab = CheckBoxRoomLab.Checked,
            };

            // Check to see if we have any resources with these specifications
            foreach (Resource existing in Program.Database.Resources)
                if (existing.Program == resource.Program && existing.Cad == resource.Cad && existing.Multi == resource.Multi &&
                    existing.Gaming == resource.Gaming && existing.Smartboard == resource.Smartboard && existing.PodWithProj == resource.PodWithProj &&
                    existing.Classroom == resource.Classroom && existing.Instruct == resource.Instruct && existing.Lab == resource.Lab)
                {
                    resource = existing;
                    break;
                }

            Target.ResourceID = resource.ResourceID;

            if (Program.Database.Resources.Find(resource.ResourceID) == null)
                Program.Database.Resources.Add(resource);

            Program.Database.SaveChanges();
        }
        
        /// <summary>
        /// Called when the user presses the delete button to delete the room from the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDeleteRoom_Click(object sender, EventArgs e)
        {
            Close();

            Program.Database.Rooms.Remove(Target);

            if (Target.Resource != null)
                Program.Database.Resources.Remove(Target.Resource);

            Program.Database.SaveChanges();
        }
    }
}
