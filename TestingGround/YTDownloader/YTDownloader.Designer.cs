namespace TestingGround
{
    partial class YTDownloader
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.textbox_url = new System.Windows.Forms.TextBox();
            this.button_download = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textbox_url
            // 
            this.textbox_url.Location = new System.Drawing.Point(0, 0);
            this.textbox_url.Name = "textbox_url";
            this.textbox_url.Size = new System.Drawing.Size(642, 20);
            this.textbox_url.TabIndex = 1;
            // 
            // button_download
            // 
            this.button_download.Location = new System.Drawing.Point(642, -1);
            this.button_download.Name = "button_download";
            this.button_download.Size = new System.Drawing.Size(63, 22);
            this.button_download.TabIndex = 2;
            this.button_download.Text = "Download";
            this.button_download.UseVisualStyleBackColor = true;
            this.button_download.Click += new System.EventHandler(this.button_download_Click);
            // 
            // YTDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 20);
            this.Controls.Add(this.button_download);
            this.Controls.Add(this.textbox_url);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "YTDownloader";
            this.Text = "YTDownloader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textbox_url;
        private System.Windows.Forms.Button button_download;
    }
}

