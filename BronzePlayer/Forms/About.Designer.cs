namespace BronzePlayer.Forms
{
    partial class About
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
			this.pictureBox_logo = new System.Windows.Forms.PictureBox();
			this.label_title = new System.Windows.Forms.Label();
			this.label_authors = new System.Windows.Forms.Label();
			this.label_contributors = new System.Windows.Forms.Label();
			this.label_version = new System.Windows.Forms.Label();
			this.button_close = new System.Windows.Forms.Button();
			this.labelLink_authors = new System.Windows.Forms.LinkLabel();
			this.labelLink_contributors = new System.Windows.Forms.LinkLabel();
			this.labelLink_version = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox_logo
			// 
			this.pictureBox_logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.pictureBox_logo.Image = global::BronzePlayer.Tralha.PNG_BronzePlayer2_2;
			this.pictureBox_logo.Location = new System.Drawing.Point(12, 12);
			this.pictureBox_logo.Name = "pictureBox_logo";
			this.pictureBox_logo.Size = new System.Drawing.Size(128, 128);
			this.pictureBox_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox_logo.TabIndex = 0;
			this.pictureBox_logo.TabStop = false;
			// 
			// label_title
			// 
			this.label_title.AutoSize = true;
			this.label_title.Font = new System.Drawing.Font("Harlow Solid Italic", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
			this.label_title.ForeColor = System.Drawing.Color.Chocolate;
			this.label_title.Location = new System.Drawing.Point(145, 9);
			this.label_title.Name = "label_title";
			this.label_title.Size = new System.Drawing.Size(192, 34);
			this.label_title.TabIndex = 1;
			this.label_title.Text = "Bronze Player";
			// 
			// label_authors
			// 
			this.label_authors.AutoSize = true;
			this.label_authors.Location = new System.Drawing.Point(148, 53);
			this.label_authors.Name = "label_authors";
			this.label_authors.Size = new System.Drawing.Size(46, 13);
			this.label_authors.TabIndex = 2;
			this.label_authors.Text = "Authors:";
			// 
			// label_contributors
			// 
			this.label_contributors.AutoSize = true;
			this.label_contributors.Location = new System.Drawing.Point(148, 73);
			this.label_contributors.Name = "label_contributors";
			this.label_contributors.Size = new System.Drawing.Size(69, 13);
			this.label_contributors.TabIndex = 3;
			this.label_contributors.Text = "Contributors: ";
			// 
			// label_version
			// 
			this.label_version.AutoSize = true;
			this.label_version.Location = new System.Drawing.Point(148, 93);
			this.label_version.Name = "label_version";
			this.label_version.Size = new System.Drawing.Size(45, 13);
			this.label_version.TabIndex = 4;
			this.label_version.Text = "Version:";
			// 
			// button_close
			// 
			this.button_close.Location = new System.Drawing.Point(358, 117);
			this.button_close.Name = "button_close";
			this.button_close.Size = new System.Drawing.Size(75, 23);
			this.button_close.TabIndex = 5;
			this.button_close.Text = "Close";
			this.button_close.UseVisualStyleBackColor = true;
			this.button_close.Click += new System.EventHandler(this.button_close_Click);
			// 
			// labelLink_authors
			// 
			this.labelLink_authors.AutoSize = true;
			this.labelLink_authors.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
			this.labelLink_authors.Location = new System.Drawing.Point(191, 53);
			this.labelLink_authors.Name = "labelLink_authors";
			this.labelLink_authors.Size = new System.Drawing.Size(46, 13);
			this.labelLink_authors.TabIndex = 6;
			this.labelLink_authors.Text = "Milkenm";
			// 
			// labelLink_contributors
			// 
			this.labelLink_contributors.AutoSize = true;
			this.labelLink_contributors.LinkArea = new System.Windows.Forms.LinkArea(4, 0);
			this.labelLink_contributors.Location = new System.Drawing.Point(211, 73);
			this.labelLink_contributors.Name = "labelLink_contributors";
			this.labelLink_contributors.Size = new System.Drawing.Size(29, 17);
			this.labelLink_contributors.TabIndex = 7;
			this.labelLink_contributors.Text = "none";
			this.labelLink_contributors.UseCompatibleTextRendering = true;
			// 
			// labelLink_version
			// 
			this.labelLink_version.AutoSize = true;
			this.labelLink_version.Location = new System.Drawing.Point(190, 93);
			this.labelLink_version.Name = "labelLink_version";
			this.labelLink_version.Size = new System.Drawing.Size(41, 13);
			this.labelLink_version.TabIndex = 8;
			this.labelLink_version.TabStop = true;
			this.labelLink_version.Text = "version";
			this.labelLink_version.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labelLink_version_LinkClicked);
			// 
			// About
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(445, 152);
			this.Controls.Add(this.labelLink_version);
			this.Controls.Add(this.labelLink_contributors);
			this.Controls.Add(this.labelLink_authors);
			this.Controls.Add(this.button_close);
			this.Controls.Add(this.label_version);
			this.Controls.Add(this.label_contributors);
			this.Controls.Add(this.label_authors);
			this.Controls.Add(this.label_title);
			this.Controls.Add(this.pictureBox_logo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "About";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "About";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_logo;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label_authors;
        private System.Windows.Forms.Label label_contributors;
        private System.Windows.Forms.Label label_version;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.LinkLabel labelLink_authors;
        private System.Windows.Forms.LinkLabel labelLink_contributors;
        private System.Windows.Forms.LinkLabel labelLink_version;
    }
}