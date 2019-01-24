namespace BronzePlayer.Forms
{
    partial class Options
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.groupbox_lang = new System.Windows.Forms.GroupBox();
            this.combobox_lang = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkbox_debug = new System.Windows.Forms.CheckBox();
            this.langBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupbox_lang.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.langBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupbox_lang
            // 
            this.groupbox_lang.Controls.Add(this.combobox_lang);
            this.groupbox_lang.Location = new System.Drawing.Point(2, 2);
            this.groupbox_lang.Name = "groupbox_lang";
            this.groupbox_lang.Size = new System.Drawing.Size(220, 46);
            this.groupbox_lang.TabIndex = 1;
            this.groupbox_lang.TabStop = false;
            this.groupbox_lang.Text = "Language";
            // 
            // combobox_lang
            // 
            this.combobox_lang.FormattingEnabled = true;
            this.combobox_lang.Location = new System.Drawing.Point(6, 19);
            this.combobox_lang.Name = "combobox_lang";
            this.combobox_lang.Size = new System.Drawing.Size(208, 21);
            this.combobox_lang.TabIndex = 0;
            this.combobox_lang.SelectedIndexChanged += new System.EventHandler(this.combobox_lang_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkbox_debug);
            this.groupBox1.Location = new System.Drawing.Point(2, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 40);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DE3UG";
            // 
            // checkbox_debug
            // 
            this.checkbox_debug.AutoSize = true;
            this.checkbox_debug.Location = new System.Drawing.Point(8, 17);
            this.checkbox_debug.Name = "checkbox_debug";
            this.checkbox_debug.Size = new System.Drawing.Size(99, 17);
            this.checkbox_debug.TabIndex = 0;
            this.checkbox_debug.Text = "Enable DE3UG";
            this.checkbox_debug.UseVisualStyleBackColor = true;
            this.checkbox_debug.CheckedChanged += new System.EventHandler(this.checkbox_debug_CheckedChanged);
            // 
            // langBindingSource
            // 
            this.langBindingSource.DataSource = typeof(Lang);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 93);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupbox_lang);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Options";
            this.Text = "Options";
            this.groupbox_lang.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.langBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupbox_lang;
        private System.Windows.Forms.ComboBox combobox_lang;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkbox_debug;
        private System.Windows.Forms.BindingSource langBindingSource;
    }
}