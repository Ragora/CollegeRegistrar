namespace RegistrarConsole
{
    partial class FormFacultyDetails
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CheckedListBoxFacultyMajors = new System.Windows.Forms.CheckedListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TextBoxFacultyPrimaryEMail = new System.Windows.Forms.TextBox();
            this.TextBoxFacultySecondaryEMail = new System.Windows.Forms.TextBox();
            this.TextBoxFacultySecondaryNumber = new System.Windows.Forms.TextBox();
            this.TextBoxFacultyPrimaryNumber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ButtonDeleteFaculty = new System.Windows.Forms.Button();
            this.ButtonCancelFaculty = new System.Windows.Forms.Button();
            this.ButtonSaveFaculty = new System.Windows.Forms.Button();
            this.TextBoxFacultyLastName = new System.Windows.Forms.TextBox();
            this.TextBoxFacultyFirstName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.CheckedListBoxFacultyMajors);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.TextBoxFacultyPrimaryEMail);
            this.groupBox1.Controls.Add(this.TextBoxFacultySecondaryEMail);
            this.groupBox1.Controls.Add(this.TextBoxFacultySecondaryNumber);
            this.groupBox1.Controls.Add(this.TextBoxFacultyPrimaryNumber);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ButtonDeleteFaculty);
            this.groupBox1.Controls.Add(this.ButtonCancelFaculty);
            this.groupBox1.Controls.Add(this.ButtonSaveFaculty);
            this.groupBox1.Controls.Add(this.TextBoxFacultyLastName);
            this.groupBox1.Controls.Add(this.TextBoxFacultyFirstName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 241);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Faculty Details";
            // 
            // CheckedListBoxFacultyMajors
            // 
            this.CheckedListBoxFacultyMajors.FormattingEnabled = true;
            this.CheckedListBoxFacultyMajors.Location = new System.Drawing.Point(106, 173);
            this.CheckedListBoxFacultyMajors.Name = "CheckedListBoxFacultyMajors";
            this.CheckedListBoxFacultyMajors.Size = new System.Drawing.Size(241, 64);
            this.CheckedListBoxFacultyMajors.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 179);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Major:";
            // 
            // TextBoxFacultyPrimaryEMail
            // 
            this.TextBoxFacultyPrimaryEMail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxFacultyPrimaryEMail.Location = new System.Drawing.Point(86, 69);
            this.TextBoxFacultyPrimaryEMail.Name = "TextBoxFacultyPrimaryEMail";
            this.TextBoxFacultyPrimaryEMail.Size = new System.Drawing.Size(342, 20);
            this.TextBoxFacultyPrimaryEMail.TabIndex = 14;
            this.TextBoxFacultyPrimaryEMail.Leave += new System.EventHandler(this.TextBoxFacultyEMail_Leave);
            // 
            // TextBoxFacultySecondaryEMail
            // 
            this.TextBoxFacultySecondaryEMail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxFacultySecondaryEMail.Location = new System.Drawing.Point(106, 121);
            this.TextBoxFacultySecondaryEMail.Name = "TextBoxFacultySecondaryEMail";
            this.TextBoxFacultySecondaryEMail.Size = new System.Drawing.Size(322, 20);
            this.TextBoxFacultySecondaryEMail.TabIndex = 13;
            this.TextBoxFacultySecondaryEMail.Leave += new System.EventHandler(this.TextBoxFacultyEMail_Leave);
            // 
            // TextBoxFacultySecondaryNumber
            // 
            this.TextBoxFacultySecondaryNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxFacultySecondaryNumber.Location = new System.Drawing.Point(106, 147);
            this.TextBoxFacultySecondaryNumber.Name = "TextBoxFacultySecondaryNumber";
            this.TextBoxFacultySecondaryNumber.Size = new System.Drawing.Size(322, 20);
            this.TextBoxFacultySecondaryNumber.TabIndex = 12;
            this.TextBoxFacultySecondaryNumber.Leave += new System.EventHandler(this.TextBoxFacultySecondaryNumber_Leave);
            // 
            // TextBoxFacultyPrimaryNumber
            // 
            this.TextBoxFacultyPrimaryNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxFacultyPrimaryNumber.Location = new System.Drawing.Point(86, 95);
            this.TextBoxFacultyPrimaryNumber.Name = "TextBoxFacultyPrimaryNumber";
            this.TextBoxFacultyPrimaryNumber.Size = new System.Drawing.Size(342, 20);
            this.TextBoxFacultyPrimaryNumber.TabIndex = 11;
            this.TextBoxFacultyPrimaryNumber.Leave += new System.EventHandler(this.TextBoxFacultyPrimaryNumber_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 150);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Secondary Phone:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Secondary E-Mail:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Primary Phone:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Primary E-Mail";
            // 
            // ButtonDeleteFaculty
            // 
            this.ButtonDeleteFaculty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonDeleteFaculty.Location = new System.Drawing.Point(10, 207);
            this.ButtonDeleteFaculty.Name = "ButtonDeleteFaculty";
            this.ButtonDeleteFaculty.Size = new System.Drawing.Size(75, 23);
            this.ButtonDeleteFaculty.TabIndex = 6;
            this.ButtonDeleteFaculty.Text = "Delete";
            this.ButtonDeleteFaculty.UseVisualStyleBackColor = true;
            this.ButtonDeleteFaculty.Click += new System.EventHandler(this.ButtonDeleteFaculty_Click);
            // 
            // ButtonCancelFaculty
            // 
            this.ButtonCancelFaculty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCancelFaculty.Location = new System.Drawing.Point(353, 207);
            this.ButtonCancelFaculty.Name = "ButtonCancelFaculty";
            this.ButtonCancelFaculty.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancelFaculty.TabIndex = 5;
            this.ButtonCancelFaculty.Text = "Cancel";
            this.ButtonCancelFaculty.UseVisualStyleBackColor = true;
            this.ButtonCancelFaculty.Click += new System.EventHandler(this.ButtonCancelFaculty_Click);
            // 
            // ButtonSaveFaculty
            // 
            this.ButtonSaveFaculty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonSaveFaculty.Location = new System.Drawing.Point(178, 207);
            this.ButtonSaveFaculty.Name = "ButtonSaveFaculty";
            this.ButtonSaveFaculty.Size = new System.Drawing.Size(75, 23);
            this.ButtonSaveFaculty.TabIndex = 4;
            this.ButtonSaveFaculty.Text = "Save";
            this.ButtonSaveFaculty.UseVisualStyleBackColor = true;
            this.ButtonSaveFaculty.Click += new System.EventHandler(this.ButtonSaveFaculty_Click);
            // 
            // TextBoxFacultyLastName
            // 
            this.TextBoxFacultyLastName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxFacultyLastName.Location = new System.Drawing.Point(72, 40);
            this.TextBoxFacultyLastName.Name = "TextBoxFacultyLastName";
            this.TextBoxFacultyLastName.Size = new System.Drawing.Size(356, 20);
            this.TextBoxFacultyLastName.TabIndex = 3;
            // 
            // TextBoxFacultyFirstName
            // 
            this.TextBoxFacultyFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxFacultyFirstName.Location = new System.Drawing.Point(72, 13);
            this.TextBoxFacultyFirstName.Name = "TextBoxFacultyFirstName";
            this.TextBoxFacultyFirstName.Size = new System.Drawing.Size(356, 20);
            this.TextBoxFacultyFirstName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Last Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "First Name:";
            // 
            // FormFacultyDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 265);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(474, 303);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(474, 303);
            this.Name = "FormFacultyDetails";
            this.Text = "Registrar Console: Faculty Details";
            this.Load += new System.EventHandler(this.FormFacultyDetails_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextBoxFacultyLastName;
        private System.Windows.Forms.TextBox TextBoxFacultyFirstName;
        private System.Windows.Forms.Button ButtonDeleteFaculty;
        private System.Windows.Forms.Button ButtonCancelFaculty;
        private System.Windows.Forms.Button ButtonSaveFaculty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TextBoxFacultyPrimaryEMail;
        private System.Windows.Forms.TextBox TextBoxFacultySecondaryEMail;
        private System.Windows.Forms.TextBox TextBoxFacultySecondaryNumber;
        private System.Windows.Forms.TextBox TextBoxFacultyPrimaryNumber;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckedListBox CheckedListBoxFacultyMajors;
    }
}