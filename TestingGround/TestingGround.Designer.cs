namespace TestingGround
{
    partial class TestingGround
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
            this.button_start = new System.Windows.Forms.Button();
            this.listbox_programs = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button_start
            // 
            this.button_start.Font = new System.Drawing.Font("Mistral", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_start.Location = new System.Drawing.Point(-1, 137);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(220, 30);
            this.button_start.TabIndex = 0;
            this.button_start.Text = "START";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // listbox_programs
            // 
            this.listbox_programs.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listbox_programs.FormattingEnabled = true;
            this.listbox_programs.ItemHeight = 15;
            this.listbox_programs.Location = new System.Drawing.Point(0, 0);
            this.listbox_programs.Name = "listbox_programs";
            this.listbox_programs.Size = new System.Drawing.Size(218, 139);
            this.listbox_programs.TabIndex = 1;
            // 
            // TestingGround
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 166);
            this.Controls.Add(this.listbox_programs);
            this.Controls.Add(this.button_start);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TestingGround";
            this.Text = "TestingGround";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.ListBox listbox_programs;
    }
}