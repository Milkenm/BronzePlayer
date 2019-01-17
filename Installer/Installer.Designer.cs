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
            this.button_install = new System.Windows.Forms.Button();
            this.textbox_path = new System.Windows.Forms.TextBox();
            this.button_path = new System.Windows.Forms.Button();
            this.progressbar_install = new System.Windows.Forms.ProgressBar();
            this.folderdialog = new System.Windows.Forms.FolderBrowserDialog();
            this.checkbox_startshortcut = new System.Windows.Forms.CheckBox();
            this.checkbox_desktopshortcut = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button_install
            // 
            this.button_install.BackColor = System.Drawing.SystemColors.Control;
            this.button_install.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_install.Location = new System.Drawing.Point(196, 32);
            this.button_install.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button_install.Name = "button_install";
            this.button_install.Size = new System.Drawing.Size(149, 38);
            this.button_install.TabIndex = 0;
            this.button_install.Text = "Install";
            this.button_install.UseVisualStyleBackColor = false;
            this.button_install.Click += new System.EventHandler(this.button_install_Click);
            // 
            // textbox_path
            // 
            this.textbox_path.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textbox_path.Location = new System.Drawing.Point(1, 1);
            this.textbox_path.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textbox_path.Name = "textbox_path";
            this.textbox_path.Size = new System.Drawing.Size(288, 20);
            this.textbox_path.TabIndex = 1;
            this.textbox_path.Leave += new System.EventHandler(this.textbox_path_Leave);
            // 
            // button_path
            // 
            this.button_path.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_path.Location = new System.Drawing.Point(289, 0);
            this.button_path.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button_path.Name = "button_path";
            this.button_path.Size = new System.Drawing.Size(56, 22);
            this.button_path.TabIndex = 2;
            this.button_path.Text = "Change";
            this.button_path.UseVisualStyleBackColor = true;
            this.button_path.Click += new System.EventHandler(this.button_path_Click);
            // 
            // progressbar_install
            // 
            this.progressbar_install.Location = new System.Drawing.Point(1, 22);
            this.progressbar_install.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.progressbar_install.Maximum = 4;
            this.progressbar_install.Name = "progressbar_install";
            this.progressbar_install.Size = new System.Drawing.Size(343, 10);
            this.progressbar_install.TabIndex = 3;
            // 
            // checkbox_startshortcut
            // 
            this.checkbox_startshortcut.AutoSize = true;
            this.checkbox_startshortcut.Checked = true;
            this.checkbox_startshortcut.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkbox_startshortcut.Location = new System.Drawing.Point(4, 35);
            this.checkbox_startshortcut.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkbox_startshortcut.Name = "checkbox_startshortcut";
            this.checkbox_startshortcut.Size = new System.Drawing.Size(121, 17);
            this.checkbox_startshortcut.TabIndex = 0;
            this.checkbox_startshortcut.Text = "Start Menu Shortcut";
            this.checkbox_startshortcut.UseVisualStyleBackColor = true;
            // 
            // checkbox_desktopshortcut
            // 
            this.checkbox_desktopshortcut.AutoSize = true;
            this.checkbox_desktopshortcut.Location = new System.Drawing.Point(4, 51);
            this.checkbox_desktopshortcut.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.checkbox_desktopshortcut.Name = "checkbox_desktopshortcut";
            this.checkbox_desktopshortcut.Size = new System.Drawing.Size(109, 17);
            this.checkbox_desktopshortcut.TabIndex = 1;
            this.checkbox_desktopshortcut.Text = "Desktop Shortcut";
            this.checkbox_desktopshortcut.UseVisualStyleBackColor = true;
            // 
            // Installer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 70);
            this.Controls.Add(this.checkbox_desktopshortcut);
            this.Controls.Add(this.checkbox_startshortcut);
            this.Controls.Add(this.progressbar_install);
            this.Controls.Add(this.button_path);
            this.Controls.Add(this.textbox_path);
            this.Controls.Add(this.button_install);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Installer";
            this.Text = "Installer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_install;
        private System.Windows.Forms.TextBox textbox_path;
        private System.Windows.Forms.Button button_path;
        private System.Windows.Forms.ProgressBar progressbar_install;
        private System.Windows.Forms.FolderBrowserDialog folderdialog;
        private System.Windows.Forms.CheckBox checkbox_desktopshortcut;
        private System.Windows.Forms.CheckBox checkbox_startshortcut;
    }
}