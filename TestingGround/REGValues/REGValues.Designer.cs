namespace TestingGround.REGValues
{
    partial class REGValues
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
            this.textbox_regpath = new System.Windows.Forms.TextBox();
            this.button_get = new System.Windows.Forms.Button();
            this.listbox_values = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // textbox_regpath
            // 
            this.textbox_regpath.Location = new System.Drawing.Point(1, 1);
            this.textbox_regpath.Name = "textbox_regpath";
            this.textbox_regpath.Size = new System.Drawing.Size(342, 20);
            this.textbox_regpath.TabIndex = 0;
            // 
            // button_get
            // 
            this.button_get.Location = new System.Drawing.Point(343, 0);
            this.button_get.Name = "button_get";
            this.button_get.Size = new System.Drawing.Size(47, 22);
            this.button_get.TabIndex = 1;
            this.button_get.Text = "Get";
            this.button_get.UseVisualStyleBackColor = true;
            this.button_get.Click += new System.EventHandler(this.button_get_Click);
            // 
            // listbox_values
            // 
            this.listbox_values.FormattingEnabled = true;
            this.listbox_values.Location = new System.Drawing.Point(1, 22);
            this.listbox_values.Name = "listbox_values";
            this.listbox_values.Size = new System.Drawing.Size(388, 199);
            this.listbox_values.TabIndex = 2;
            // 
            // REGValues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 222);
            this.Controls.Add(this.listbox_values);
            this.Controls.Add(this.button_get);
            this.Controls.Add(this.textbox_regpath);
            this.Name = "REGValues";
            this.Text = "REGValues";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textbox_regpath;
        private System.Windows.Forms.Button button_get;
        private System.Windows.Forms.ListBox listbox_values;
    }
}