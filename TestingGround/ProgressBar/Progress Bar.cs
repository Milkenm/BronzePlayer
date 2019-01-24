using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace TestingGround.Background_Worker_Progress
{
    public partial class ProgressBar : Form
    {
        public ProgressBar()
        {
            InitializeComponent();
        }

        private void backgroundworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button_download.Enabled = true;
        }

        private void button_download_Click(object sender, EventArgs e)
        {
            button_download.Enabled = false;
            backgroundworker.RunWorkerAsync();
        }

        private void backgroundworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label_progress.Text = e.ProgressPercentage.ToString();
        }

        private void backgroundworker_DoWork(object sender, DoWorkEventArgs e)
        {
            StartProcessing();
        }

        private void StartProcessing()
        {
            Thread procThread = new Thread(Process);

            procThread.Start();
        }

        private void Process()
        {
            string dir = @"C:\Windows\System32\";

            int progress = 0;
            int percentage = 0;
            int files = Directory.GetFiles(dir).Length;

            this.Invoke(new Action(() =>
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = files;
                MessageBox.Show("Found: " + progressBar1.Maximum.ToString() + " files");
            }));

            foreach(string f in Directory.GetFiles(dir))
            {
                progress = progress + 1;
                percentage = progress * 100 / files;

                this.Invoke(new Action(()=>
                {
                    label_progress.Text = percentage + "%";

                    listBox1.Items.Add(f);
                    listBox1.SelectedIndex = listBox1.Items.IndexOf(f);
                    progressBar1.Value = progress;
                }));
            }

            MessageBox.Show("Complete!");
        }
    }
}
