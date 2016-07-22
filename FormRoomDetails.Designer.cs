namespace RegistrarConsole
{
    partial class FormRoomDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LabelRoomResources = new System.Windows.Forms.Label();
            this.GroupBoxRoomDetails = new System.Windows.Forms.GroupBox();
            this.ButtonSaveRoom = new System.Windows.Forms.Button();
            this.ButtonCancelRoom = new System.Windows.Forms.Button();
            this.ButtonDeleteRoom = new System.Windows.Forms.Button();
            this.CheckedListBoxRoomResources = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CheckBoxRoomLab = new System.Windows.Forms.CheckBox();
            this.CheckBoxRoomInstructional = new System.Windows.Forms.CheckBox();
            this.CheckBoxRoomClassroom = new System.Windows.Forms.CheckBox();
            this.GroupBoxRoomDetails.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelRoomResources
            // 
            this.LabelRoomResources.AutoSize = true;
            this.LabelRoomResources.Location = new System.Drawing.Point(177, 16);
            this.LabelRoomResources.Name = "LabelRoomResources";
            this.LabelRoomResources.Size = new System.Drawing.Size(107, 13);
            this.LabelRoomResources.TabIndex = 0;
            this.LabelRoomResources.Text = "Resources Available:";
            // 
            // GroupBoxRoomDetails
            // 
            this.GroupBoxRoomDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBoxRoomDetails.Controls.Add(this.groupBox1);
            this.GroupBoxRoomDetails.Controls.Add(this.ButtonSaveRoom);
            this.GroupBoxRoomDetails.Controls.Add(this.ButtonCancelRoom);
            this.GroupBoxRoomDetails.Controls.Add(this.ButtonDeleteRoom);
            this.GroupBoxRoomDetails.Controls.Add(this.CheckedListBoxRoomResources);
            this.GroupBoxRoomDetails.Controls.Add(this.LabelRoomResources);
            this.GroupBoxRoomDetails.Location = new System.Drawing.Point(12, 12);
            this.GroupBoxRoomDetails.Name = "GroupBoxRoomDetails";
            this.GroupBoxRoomDetails.Size = new System.Drawing.Size(680, 358);
            this.GroupBoxRoomDetails.TabIndex = 2;
            this.GroupBoxRoomDetails.TabStop = false;
            this.GroupBoxRoomDetails.Text = "Room Details:";
            // 
            // ButtonSaveRoom
            // 
            this.ButtonSaveRoom.Location = new System.Drawing.Point(378, 329);
            this.ButtonSaveRoom.Name = "ButtonSaveRoom";
            this.ButtonSaveRoom.Size = new System.Drawing.Size(75, 23);
            this.ButtonSaveRoom.TabIndex = 4;
            this.ButtonSaveRoom.Text = "Save";
            this.ButtonSaveRoom.UseVisualStyleBackColor = true;
            this.ButtonSaveRoom.Click += new System.EventHandler(this.ButtonSaveRoom_Click);
            // 
            // ButtonCancelRoom
            // 
            this.ButtonCancelRoom.Location = new System.Drawing.Point(180, 329);
            this.ButtonCancelRoom.Name = "ButtonCancelRoom";
            this.ButtonCancelRoom.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancelRoom.TabIndex = 3;
            this.ButtonCancelRoom.Text = "Cancel";
            this.ButtonCancelRoom.UseVisualStyleBackColor = true;
            this.ButtonCancelRoom.Click += new System.EventHandler(this.ButtonCancelRoom_Click);
            // 
            // ButtonDeleteRoom
            // 
            this.ButtonDeleteRoom.Location = new System.Drawing.Point(16, 329);
            this.ButtonDeleteRoom.Name = "ButtonDeleteRoom";
            this.ButtonDeleteRoom.Size = new System.Drawing.Size(75, 23);
            this.ButtonDeleteRoom.TabIndex = 2;
            this.ButtonDeleteRoom.Text = "Delete";
            this.ButtonDeleteRoom.UseVisualStyleBackColor = true;
            this.ButtonDeleteRoom.Click += new System.EventHandler(this.ButtonDeleteRoom_Click);
            // 
            // CheckedListBoxRoomResources
            // 
            this.CheckedListBoxRoomResources.FormattingEnabled = true;
            this.CheckedListBoxRoomResources.Items.AddRange(new object[] {
            "Computers",
            "Visual Studio",
            "Junk Computers"});
            this.CheckedListBoxRoomResources.Location = new System.Drawing.Point(16, 32);
            this.CheckedListBoxRoomResources.Name = "CheckedListBoxRoomResources";
            this.CheckedListBoxRoomResources.Size = new System.Drawing.Size(437, 94);
            this.CheckedListBoxRoomResources.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CheckBoxRoomClassroom);
            this.groupBox1.Controls.Add(this.CheckBoxRoomInstructional);
            this.groupBox1.Controls.Add(this.CheckBoxRoomLab);
            this.groupBox1.Location = new System.Drawing.Point(16, 146);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 45);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Room Properties";
            // 
            // CheckBoxRoomLab
            // 
            this.CheckBoxRoomLab.AutoSize = true;
            this.CheckBoxRoomLab.Location = new System.Drawing.Point(16, 20);
            this.CheckBoxRoomLab.Name = "CheckBoxRoomLab";
            this.CheckBoxRoomLab.Size = new System.Drawing.Size(44, 17);
            this.CheckBoxRoomLab.TabIndex = 0;
            this.CheckBoxRoomLab.Text = "Lab";
            this.CheckBoxRoomLab.UseVisualStyleBackColor = true;
            // 
            // CheckBoxRoomInstructional
            // 
            this.CheckBoxRoomInstructional.AutoSize = true;
            this.CheckBoxRoomInstructional.Location = new System.Drawing.Point(354, 19);
            this.CheckBoxRoomInstructional.Name = "CheckBoxRoomInstructional";
            this.CheckBoxRoomInstructional.Size = new System.Drawing.Size(83, 17);
            this.CheckBoxRoomInstructional.TabIndex = 1;
            this.CheckBoxRoomInstructional.Text = "Instructional";
            this.CheckBoxRoomInstructional.UseVisualStyleBackColor = true;
            // 
            // CheckBoxRoomClassroom
            // 
            this.CheckBoxRoomClassroom.AutoSize = true;
            this.CheckBoxRoomClassroom.Location = new System.Drawing.Point(164, 19);
            this.CheckBoxRoomClassroom.Name = "CheckBoxRoomClassroom";
            this.CheckBoxRoomClassroom.Size = new System.Drawing.Size(82, 17);
            this.CheckBoxRoomClassroom.TabIndex = 2;
            this.CheckBoxRoomClassroom.Text = "Class Room";
            this.CheckBoxRoomClassroom.UseVisualStyleBackColor = true;
            // 
            // FormRoomDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 382);
            this.Controls.Add(this.GroupBoxRoomDetails);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(512, 230);
            this.Name = "FormRoomDetails";
            this.ShowInTaskbar = false;
            this.Text = "Registrar Console: Room Details";
            this.Load += new System.EventHandler(this.RoomDetails_Load);
            this.GroupBoxRoomDetails.ResumeLayout(false);
            this.GroupBoxRoomDetails.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LabelRoomResources;
        private System.Windows.Forms.GroupBox GroupBoxRoomDetails;
        private System.Windows.Forms.CheckedListBox CheckedListBoxRoomResources;
        private System.Windows.Forms.Button ButtonSaveRoom;
        private System.Windows.Forms.Button ButtonCancelRoom;
        private System.Windows.Forms.Button ButtonDeleteRoom;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox CheckBoxRoomClassroom;
        private System.Windows.Forms.CheckBox CheckBoxRoomInstructional;
        private System.Windows.Forms.CheckBox CheckBoxRoomLab;
    }
}