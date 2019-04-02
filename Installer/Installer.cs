#region Using
using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Windows.Forms;

using IWshRuntimeLibrary;

using Microsoft.Win32;
#endregion Using

namespace Installer
{
    public partial class Installer : Form
    {
        #region Vars
        // # ================================================================================================================================= #
        string version = "0.2.0";
        string regPath = @"Software\Milkenm\Bronze Player";
        string arch;
        bool install = false, newerInstalled = false;
        // # ================================================================================================================================= #
        #endregion Vars

        #region Functions
        // # ================================================================================================================================= #
        void AddStartMenuShortcut()
        {
            try
            {
                ///
                // https://stackoverflow.com/questions/25024785/how-to-create-start-menu-shortcut
                ///

                string pathToExe = textBox_path.Text + @"\BronzePlayer.exe";
                string commonStartMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu);
                string appStartMenuPath = Path.Combine(commonStartMenuPath, "Programs", "Milkenm");

                if (!Directory.Exists(appStartMenuPath)) // If NOT exists.
                {
                    Directory.CreateDirectory(appStartMenuPath);
                }

                string shortcutLocation = Path.Combine(appStartMenuPath, "Bronze Player.lnk");
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

                shortcut.Description = "Bronze Player";
                shortcut.IconLocation = textBox_path.Text + @"\icon.ico";
                shortcut.TargetPath = pathToExe;
                shortcut.Save();
            }
            #region DE3UG
            catch { }
            #endregion
        }
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        void AddDesktopShortcut()
        {
            try
            {
                string pathToExe = textBox_path.Text + @"\BronzePlayer.exe";
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);


                string shortcutLocation = Path.Combine(desktopPath, "Bronze Player.lnk");
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

                shortcut.Description = "Bronze Player";
                shortcut.IconLocation = textBox_path.Text + @"\Icon.ico";
                shortcut.TargetPath = pathToExe;
                shortcut.Save();
            }
            #region DE3UG
            catch { }
            #endregion
        }
        // # ================================================================================================================================= #
        #endregion Functions





        #region Load
        // # ================================================================================================================================= #
        public Installer()
        {
            try
            {
                InitializeComponent();

                this.Enabled = false;
                if (Registry.LocalMachine.OpenSubKey(regPath) != null)
                {
                    string[] installedVersion = Registry.LocalMachine.OpenSubKey(regPath, true).GetValue("Version").ToString().Split('.');
                    string[] newVersion = version.Split('.');
                    if (installedVersion != null)
                    {
                        for (int loop = 0; loop < 3; loop++) // Loop 3 times
                        {
                            if (Convert.ToInt32(newVersion[loop]) > Convert.ToInt32(installedVersion[loop]))
                            {
                                install = true;
                                break;
                            }
                            else if (Convert.ToInt32(newVersion[loop]) < Convert.ToInt32(installedVersion[loop]))
                            {
                                newerInstalled = true;
                            }
                        }
                    }
                    if (newerInstalled == true)
                    {
                        MessageBox.Show("A newer version is installed!", "Bronze Player", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    else if (install == false)
                    {
                        MessageBox.Show("This version is already installed!", "Bronze Player", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }

                    if (Registry.LocalMachine.OpenSubKey(regPath, true).GetValue("Platform").ToString() == "x32")
                    {
                        radioButton_32.Checked = true;
                        radioButton_64.Enabled = false;
                    }
                    else if (Registry.LocalMachine.OpenSubKey(regPath, true).GetValue("Platform").ToString() == "x64")
                    {
                        radioButton_64.Checked = true;
                        radioButton_32.Enabled = false;
                    }

                    button_path.Enabled = false;
                    textBox_path.ReadOnly = true;
                }
                this.Enabled = true;

                if (Environment.Is64BitOperatingSystem == true)
                {
                    arch = "x64";
                    radioButton_64.Checked = true;
                }
                else
                {
                    arch = "x32";
                    radioButton_32.Checked = true;
                    radioButton_64.Enabled = false;
                }
            }
            #region DE3UG
            catch { }
            #endregion
        }
        // # ================================================================================================================================= #
        #endregion Load



        #region Path
        // # ================================================================================================================================= #
        private void button_path_Click(object sender, EventArgs e)
        {
            try
            {
                folderDialog.ShowDialog();
                if (folderDialog.SelectedPath != null)
                {
                    textBox_path.Text = folderDialog.SelectedPath + @"\";
                }
            }
            #region DE3UG
            catch { }
            #endregion
        }
        // # ================================================================================================================================= #
        #endregion Path



        #region Install Button
        // # ================================================================================================================================= #
        private void button_install_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                progressBar.Style = ProgressBarStyle.Marquee;

                #region ???
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\Files32"))
                {
                    Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Files32", true);
                }
                if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\Files64"))
                {
                    Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Files64", true);
                }
                #endregion ???



                #region Fresh Install
                if (Registry.LocalMachine.OpenSubKey(regPath) == null)
                {
                    if (Directory.Exists(textBox_path.Text) == false)
                    {
                        Directory.CreateDirectory(textBox_path.Text);
                    }

                    Registry.LocalMachine.CreateSubKey(regPath, true);
                    Registry.LocalMachine.OpenSubKey(regPath, true).SetValue("Installed", "true", RegistryValueKind.String);
                    Registry.LocalMachine.OpenSubKey(regPath, true).SetValue("Path", textBox_path.Text, RegistryValueKind.String);
                    if (radioButton_32.Checked == true) // x32
                    {
                        Registry.LocalMachine.OpenSubKey(regPath, true).SetValue("Platform", "x32", RegistryValueKind.String);
                        ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Files32.zip", textBox_path.Text);
                    }
                    else if (radioButton_64.Checked == true) // x64
                    {
                        Registry.LocalMachine.OpenSubKey(regPath, true).SetValue("Platform", "x64", RegistryValueKind.String);
                        ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Files64.zip", textBox_path.Text);
                    }
                    System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Files32.zip");
                    System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Files64.zip");

                    progressBar.Style = ProgressBarStyle.Blocks;
                }
                #endregion Fresh Install
                #region Update Install
                else // If installed
                {
                    string path = Registry.LocalMachine.OpenSubKey(regPath, true).GetValue("Path").ToString();
                    string arch = Registry.LocalMachine.OpenSubKey(regPath, true).GetValue("Platform").ToString();

                    #region Remove Old Installation Files
                    foreach (var filePath in Directory.GetFiles(path))
                    {
                        if (Path.GetFileNameWithoutExtension(filePath) != "BD" && Path.GetFileNameWithoutExtension(filePath) != "BronzePlayer.exe")
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                    Thread.Sleep(2000);
                    #endregion Remove Old Installation Files

                    #region Install x32
                    if (arch == "x32")
                    {
                        ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Files32zip", AppDomain.CurrentDomain.BaseDirectory + @"\Files32");
                        foreach (string file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"\Files32"))
                        {
                            string fileName = Path.GetFileName(file);
                            if (System.IO.File.Exists(path + fileName) == false)
                            {
                                System.IO.File.Copy(file, path + fileName);
                            }

                            Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Files32", true);
                        }
                    }
                    #endregion Install x32
                    #region Install x64
                    else if (arch == "x64")
                    {

                        ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Files64.zip", AppDomain.CurrentDomain.BaseDirectory + @"\Files64");

                        foreach (string file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"\Files64"))
                        {
                            string fileName = Path.GetFileName(file);
                            if (System.IO.File.Exists(path + fileName) == false)
                            {
                                System.IO.File.Copy(file, path + fileName);
                            }
                        }

                        Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Files64", true);
                    }
                    #endregion Install x64
                }
                #endregion Update Install



                #region Create Shortcuts
                if (checkBox_startShortcut.Checked == true)
                {
                    AddStartMenuShortcut();
                }
                if (checkBox_desktopIcon.Checked == true)
                {
                    AddDesktopShortcut();
                }
                #endregion Create Shortcuts

                Registry.LocalMachine.OpenSubKey(regPath, true).SetValue("Version", version, RegistryValueKind.String);
                progressBar.Style = ProgressBarStyle.Blocks;
                MessageBox.Show("Bronze Player (v" + version + ") has been installed!", "Bronze Player", MessageBoxButtons.OK, MessageBoxIcon.Information);

                #region Delete Installation Files
                foreach (string file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory))
                {
                    if (file != AppDomain.CurrentDomain.BaseDirectory + "Installer.exe")
                    {
                        System.IO.File.Delete(file);
                    }
                }
                #endregion Delete Installation Files
                Environment.Exit(0);
            }
            catch { }
        }
        // # ================================================================================================================================= #
        #endregion Install Button



        #region 32/64 Picker
        // # ================================================================================================================================= #
        private void radioButton_32_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton_64.Checked == false)
                {
                    radioButton_64.Checked = false;
                    if (arch == "x32")
                    {
                        textBox_path.Text = Environment.GetEnvironmentVariable("ProgramFiles") + @"\Milkenm\Bronze Player\";
                    }
                    else
                    {
                        textBox_path.Text = Environment.GetEnvironmentVariable("ProgramFiles(x86)") + @"\Milkenm\Bronze Player\";
                    }
                }
            }
            #region DE3UG
            catch { }
            #endregion
        }
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        private void radioButton_64_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioButton_32.Checked == false)
                {
                    radioButton_32.Checked = false;
                    textBox_path.Text = Environment.GetEnvironmentVariable("ProgramFiles") + @"\Milkenm\Bronze Player\";
                }
            }
            #region DE3UG
            catch { }
            #endregion
        }
        // # ================================================================================================================================= #
        #endregion 32/64 Picker
    }
}
