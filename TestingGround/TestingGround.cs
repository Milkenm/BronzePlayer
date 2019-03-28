using System;
using System.Windows.Forms;

namespace TestingGround
{
    public partial class TestingGround : Form
    {
        #region Load / Unload
        public TestingGround()
        {
            InitializeComponent();

            listbox_programs.Items.Add("YTDownloader");
            listbox_programs.Items.Add("UserControls");
            listbox_programs.Items.Add("Background Worker Progress");
            listbox_programs.Items.Add("Regedit Values");
            listbox_programs.Items.Add("Timer");
        }
        #endregion Load / Unload



        #region Program Selector
        private void button_start_Click(object sender, EventArgs e)
        {
            #region YTDownloader
            if (listbox_programs.SelectedIndex == 0)
            {
                YTDownloader.YTDownloader YTD = new YTDownloader.YTDownloader();
                YTD.Show();
            }
            #endregion YTDownloader

            #region UserControls
            else if (listbox_programs.SelectedIndex == 1)
            {
                UserControls.UserControls UC = new UserControls.UserControls();
                UC.Show();
            }
            #endregion UserControls

            #region ProgressBar
            else if (listbox_programs.SelectedIndex == 2)
            {
                Background_Worker_Progress.ProgressBar BWP = new Background_Worker_Progress.ProgressBar();
                BWP.Show();
            }
            #endregion ProgressBar

            #region REGValues
            else if (listbox_programs.SelectedIndex == 3)
            {
                var REGV = new REGValues.REGValues();
                REGV.Show();
            }
            #endregion REGValue

            #region Timer
            else if (listbox_programs.SelectedIndex == 4)
            {
                var T = new Timer.Timer();
                T.Show();
            }
            #endregion Timer
        }
        #endregion Program Selector
    }
}
