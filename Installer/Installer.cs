using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace Installer
{
    public partial class Installer : Form
    {
        #region Vars
        string lastPath;
        #endregion Vars
        














        #region Load / Unload
        // # ================================================================================================================================= #
        public Installer()
        {
            InitializeComponent();

            button_install.Enabled = false;

            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\BronzePlayer");

            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)))
            {
                textbox_path.Text = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)+"\\BronzePlayer";
                lastPath = textbox_path.Text;
                button_install.Enabled = true;
            }
        }
        // # ================================================================================================================================= #
        #endregion Load / Unload


        #region Path
        // # ================================================================================================================================= #
        private void button_path_Click(object sender, EventArgs e)
        {
            if (folderdialog.ShowDialog() == DialogResult.OK)
            {
                if (Directory.Exists(folderdialog.SelectedPath))
                {
                    textbox_path.Text = folderdialog.SelectedPath;
                    lastPath = textbox_path.Text;
                    button_install.Enabled = true;
                }
                else
                {
                    MessageBox.Show("That directory does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        private void textbox_path_Leave(object sender, EventArgs e)
        {
            if (!Directory.Exists(textbox_path.Text)) // If NOT exists.
            {
                MessageBox.Show("That directory does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textbox_path.Text = lastPath;
            }
            else
            {
                if (button_install.Enabled == false)
                {
                    button_install.Enabled = true;
                }
            }
        }
        // # ================================================================================================================================= #
        #endregion


        #region Installation
        // # ================================================================================================================================= #
        private void button_install_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textbox_path.Text))
            {
                if (checkbox_startshortcut.Checked == true)
                {
                    progressbar_install.Maximum++;
                }
                if (checkbox_desktopshortcut.Checked == true)
                {
                    progressbar_install.Maximum++;
                }

                textbox_path.ReadOnly = true;
                button_install.Enabled = false;
                button_path.Enabled = false;

                button_install.Text = "Installing...";

                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Bronze Player\\temp");
                progressbar_install.Value++;
                System.IO.File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Bronze Player\\temp\\installation.zip", Program_Files.Release);
                progressbar_install.Value++;
                ZipFile.ExtractToDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Bronze Player\\temp\\installation.zip", textbox_path.Text);
                progressbar_install.Value++;

                if (checkbox_startshortcut.Checked == true)
                {
                    AddStartMenuShortcut();
                    progressbar_install.Value++;
                }
                if (checkbox_desktopshortcut.Checked == true)
                {
                    AddDesktopShortcut();
                    progressbar_install.Value++;
                }
                
                Thread.Sleep(1000); // Let the bar fill up.

                MessageBox.Show("Bronze Player was successfully installed!", "Installation complete!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        void AddStartMenuShortcut()
        {
            ///
            // https://stackoverflow.com/questions/25024785/how-to-create-start-menu-shortcut
            ///

            string pathToExe = textbox_path.Text + "\\BronzePlayer.exe";
            string commonStartMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
            string appStartMenuPath = Path.Combine(commonStartMenuPath, "Programs", "Bronze Player");

            if (!Directory.Exists(appStartMenuPath)) // If NOT exists.
            {
                Directory.CreateDirectory(appStartMenuPath);
            }

            string shortcutLocation = Path.Combine(appStartMenuPath, "Bronzeplayer.lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "Bronze Player";
            shortcut.IconLocation = textbox_path.Text + "\\icon.ico";
            shortcut.TargetPath = pathToExe;
            shortcut.Save();
        }
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        void AddDesktopShortcut()
        {
            string pathToExe = textbox_path.Text + "\\BronzePlayer.exe";
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            

            string shortcutLocation = Path.Combine(desktopPath, "Bronze Player.lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "Bronze Player";
            shortcut.IconLocation = textbox_path.Text + "\\icon.ico";
            shortcut.TargetPath = pathToExe;
            shortcut.Save();
        }
        #endregion Installation
    }
}
