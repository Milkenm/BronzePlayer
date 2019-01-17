namespace TestingGround.Background_Worker_Progress
{
    partial class Background_Worker_Progress
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
            this.button_download = new System.Windows.Forms.Button();
            this.backgroundworker = new System.ComponentModel.BackgroundWorker();
            this.label_progress = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // button_download
            // 
            this.button_download.Location = new System.Drawing.Point(268, 1);
            this.button_download.Name = "button_download";
            this.button_download.Size = new System.Drawing.Size(75, 23);
            this.button_download.TabIndex = 0;
            this.button_download.Text = "Download";
            this.button_download.UseVisualStyleBackColor = true;
            this.button_download.Click += new System.EventHandler(this.button_download_Click);
            // 
            // backgroundworker
            // 
            this.backgroundworker.WorkerReportsProgress = true;
            this.backgroundworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundworker_DoWork);
            this.backgroundworker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundworker_ProgressChanged);
            this.backgroundworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundworker_RunWorkerCompleted);
            // 
            // label_progress
            // 
            this.label_progress.AutoSize = true;
            this.label_progress.Location = new System.Drawing.Point(180, 21);
            this.label_progress.Name = "label_progress";
            this.label_progress.Size = new System.Drawing.Size(21, 13);
            this.label_progress.TabIndex = 1;
            this.label_progress.Text = "0%";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 54);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(428, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // Background_Worker_Progress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 382);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label_progress);
            this.Controls.Add(this.button_download);
            this.Name = "Background_Worker_Progress";
            this.Text = "Background_Worker_Progress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_download;
        private System.ComponentModel.BackgroundWorker backgroundworker;
        private System.Windows.Forms.Label label_progress;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}