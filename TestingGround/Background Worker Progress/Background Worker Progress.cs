using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestingGround.Background_Worker_Progress
{
    public partial class Background_Worker_Progress : Form
    {
        public Background_Worker_Progress()
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
            FakeCountingWork();
        }

        private void FakeCountingWork()
        {
            int totalNumber = 100;
            int progressCounter = 0;
            while (progressCounter < totalNumber)
            {
                int fakecounter = 0;
                for (int x=0;x < 100000000; x++)
                {
                    fakecounter++;
                }
                progressCounter++;
                backgroundworker.ReportProgress(progressCounter);
                progressBar1.Value = progressCounter;
            }
        }
    }
}
