namespace Installer
{
    partial class Installer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Installer));
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.button_path = new System.Windows.Forms.Button();
            this.button_install = new System.Windows.Forms.Button();
            this.checkBox_startShortcut = new System.Windows.Forms.CheckBox();
            this.checkBox_desktopIcon = new System.Windows.Forms.CheckBox();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.radioButton_32 = new System.Windows.Forms.RadioButton();
            this.radioButton_64 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_path
            // 
            this.textBox_path.Location = new System.Drawing.Point(1, 1);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.Size = new System.Drawing.Size(268, 20);
            this.textBox_path.TabIndex = 0;
            // 
            // button_path
            // 
            this.button_path.Location = new System.Drawing.Point(269, 0);
            this.button_path.Name = "button_path";
            this.button_path.Size = new System.Drawing.Size(79, 22);
            this.button_path.TabIndex = 1;
            this.button_path.Text = "Change";
            this.button_path.UseVisualStyleBackColor = true;
            this.button_path.Click += new System.EventHandler(this.button_path_Click);
            // 
            // button_install
            // 
            this.button_install.Location = new System.Drawing.Point(269, 52);
            this.button_install.Name = "button_install";
            this.button_install.Size = new System.Drawing.Size(79, 23);
            this.button_install.TabIndex = 2;
            this.button_install.Text = "Install";
            this.button_install.UseVisualStyleBackColor = true;
            this.button_install.Click += new System.EventHandler(this.button_install_Click);
            // 
            // checkBox_startShortcut
            // 
            this.checkBox_startShortcut.AutoSize = true;
            this.checkBox_startShortcut.Checked = true;
            this.checkBox_startShortcut.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_startShortcut.Location = new System.Drawing.Point(2, 22);
            this.checkBox_startShortcut.Name = "checkBox_startShortcut";
            this.checkBox_startShortcut.Size = new System.Drawing.Size(121, 17);
            this.checkBox_startShortcut.TabIndex = 3;
            this.checkBox_startShortcut.Text = "Start Menu Shortcut";
            this.checkBox_startShortcut.UseVisualStyleBackColor = true;
            // 
            // checkBox_desktopIcon
            // 
            this.checkBox_desktopIcon.AutoSize = true;
            this.checkBox_desktopIcon.Checked = true;
            this.checkBox_desktopIcon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_desktopIcon.Location = new System.Drawing.Point(2, 37);
            this.checkBox_desktopIcon.Name = "checkBox_desktopIcon";
            this.checkBox_desktopIcon.Size = new System.Drawing.Size(90, 17);
            this.checkBox_desktopIcon.TabIndex = 4;
            this.checkBox_desktopIcon.Text = "Desktop Icon";
            this.checkBox_desktopIcon.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(1, 53);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(268, 21);
            this.progressBar.TabIndex = 7;
            // 
            // radioButton_32
            // 
            this.radioButton_32.AutoSize = true;
            this.radioButton_32.Location = new System.Drawing.Point(9, 2);
            this.radioButton_32.Name = "radioButton_32";
            this.radioButton_32.Size = new System.Drawing.Size(56, 17);
            this.radioButton_32.TabIndex = 8;
            this.radioButton_32.Text = "32 bits";
            this.radioButton_32.UseVisualStyleBackColor = true;
            this.radioButton_32.CheckedChanged += new System.EventHandler(this.radioButton_32_CheckedChanged);
            // 
            // radioButton_64
            // 
            this.radioButton_64.AutoSize = true;
            this.radioButton_64.Location = new System.Drawing.Point(9, 17);
            this.radioButton_64.Name = "radioButton_64";
            this.radioButton_64.Size = new System.Drawing.Size(56, 17);
            this.radioButton_64.TabIndex = 9;
            this.radioButton_64.Text = "64 bits";
            this.radioButton_64.UseVisualStyleBackColor = true;
            this.radioButton_64.CheckedChanged += new System.EventHandler(this.radioButton_64_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.radioButton_64);
            this.panel1.Controls.Add(this.radioButton_32);
            this.panel1.Location = new System.Drawing.Point(270, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(77, 35);
            this.panel1.TabIndex = 10;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // 
            // Installer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 75);
            this.Controls.Add(this.button_install);
            this.Controls.Add(this.button_path);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox_path);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.checkBox_desktopIcon);
            this.Controls.Add(this.checkBox_startShortcut);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Installer";
            this.Text = "Installer";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox_path;
        private System.Windows.Forms.Button button_path;
        private System.Windows.Forms.Button button_install;
        private System.Windows.Forms.CheckBox checkBox_startShortcut;
        private System.Windows.Forms.CheckBox checkBox_desktopIcon;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.RadioButton radioButton_32;
        private System.Windows.Forms.RadioButton radioButton_64;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}