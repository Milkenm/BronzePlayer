using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using Microsoft.Win32;

namespace Installer
{
    public partial class Installer : Form
    {
        #region Vars
        // # ================================================================================================================================= #
        public string version = "0.0.2";
        string regPath = @"SOFTWARE\Milkenm\BronzePlayer";
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        string lastPath;
        // # ================================================================================================================================= #
        #endregion Vars



        #region Functions
        // # ================================================================================================================================= #
        bool CheckRegistry(string _path, bool _update)
        {
            bool regExists = false;

            #region Check If Key Exists
            RegistryKey key = Registry.LocalMachine.OpenSubKey(regPath, true);

            if (key != null)
            {
                regExists = true;
                string value = key.GetValue("Installed").ToString();
            }
            #endregion Check If Key Exists

            if (regExists == false)
            {
                if (_update == true)
                {
                    Registry.LocalMachine.CreateSubKey(regPath);

                    Registry.LocalMachine.OpenSubKey(regPath).SetValue("Installed", "true", RegistryValueKind.String);
                    Registry.LocalMachine.OpenSubKey(regPath).SetValue("Version", version, RegistryValueKind.String);
                    Registry.LocalMachine.OpenSubKey(regPath).SetValue("Path", _path, RegistryValueKind.String);
                }
            }

            return regExists;
        }
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        string GetRegistry(string _folder, string _key)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(_folder, true);
            string value = key.GetValue(_key).ToString();

            return value;
        }
        // # ================================================================================================================================= #

            

        // # ================================================================================================================================= #
        void AddStartMenuShortcut()
        {
            ///
            // https://stackoverflow.com/questions/25024785/how-to-create-start-menu-shortcut
            ///

            string pathToExe = textbox_path.Text + @"\BronzePlayer.exe";
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
            shortcut.IconLocation = textbox_path.Text + @"\icon.ico";
            shortcut.TargetPath = pathToExe;
            shortcut.Save();
        }
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        void AddDesktopShortcut()
        {
            string pathToExe = textbox_path.Text + @"\BronzePlayer.exe";
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);


            string shortcutLocation = Path.Combine(desktopPath, "Bronze Player.lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "Bronze Player";
            shortcut.IconLocation = textbox_path.Text + @"\icon.ico";
            shortcut.TargetPath = pathToExe;
            shortcut.Save();
        }
        // # ================================================================================================================================= #
        #endregion Functions















        #region Load / Unload
        // # ================================================================================================================================= #
        public Installer()
        {
            InitializeComponent();

            button_install.Enabled = false;
            button_update.Enabled = false;

            this.Text = "Installer: Bronze Player (v" + version + ")";
            this.Size = new Size(360, 109);

            bool installed = CheckRegistry(null, false);
            if (installed == false) // Install
            {
                panel_update.Dispose();
                panel_install.Dock = DockStyle.Fill;

                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Bronze Player");

                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)))
                {
                    textbox_path.Text = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\Bronze Player";
                    lastPath = textbox_path.Text;
                    button_install.Enabled = true;
                }
            }
            else // Update
            {
                panel_install.Dispose();
                panel_update.Dock = DockStyle.Fill;

                if (GetRegistry(regPath, "Version") != version)
                {

                    textbox_update_path.Text = GetRegistry(regPath, "Path");

                    button_update.Enabled = true;
                }
                else
                {
                    MessageBox.Show("You already have this version installed!\nThe setup will now close.", "Bronze Player", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
        #region Install
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

                progressbar_install.Value = progressbar_install.Maximum;

                MessageBox.Show("Bronze Player was successfully installed!", "Bronze Player", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
        // # ================================================================================================================================= #
        #endregion Install



        #region Update
        private void button_update_Click(object sender, EventArgs e)
        {
            // todo
        }
        #endregion Update
        #endregion Installation
    }
}
