using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using Microsoft.Win32;

namespace Installer
{
    public partial class Installer : Form
    {
        #region Vars
        // # ================================================================================================================================= #
        string version = "0.1.0";
        string regPath = @"Software\Milkenm\Bronze Player";
        bool debug = true;
        string arch;
        bool install = false, newerInstalled = false;
        // # ================================================================================================================================= #
        #endregion Vars



        #region Functions
        // # ================================================================================================================================= #
        void AddStartMenuShortcut()
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
        // # ================================================================================================================================= #



        // # ================================================================================================================================= #
        void AddDesktopShortcut()
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
        // # ================================================================================================================================= #
        #endregion Functions



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
                            if (Convert.ToInt16(newVersion[loop]) > Convert.ToInt16(installedVersion[loop]))
                            {
                                install = true;
                            }
                            else if (Convert.ToInt16(newVersion[loop]) < Convert.ToInt16(installedVersion[loop]))
                            {
                                newerInstalled = true;
                            }
                        }
                    }
                    if (newerInstalled == true)
                    {
                        MessageBox.Show("A newer version is installed!");
                        this.Close();
                    }
                    else if (install == false)
                    {
                        MessageBox.Show("This version is already installed!");
                        this.Close();
                    }
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
            catch (Exception exception)
            {
                if (debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - Installer()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }

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
            catch (Exception exception)
            {
                if (debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - button_path_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }

        private void button_install_Click(object sender, EventArgs e)
        {
            try
            {
                if (Registry.LocalMachine.OpenSubKey(regPath) == null)
                {
                    progressBar.Style = ProgressBarStyle.Marquee;

                    if (Directory.Exists(textBox_path.Text) == false)
                    {
                        Directory.CreateDirectory(textBox_path.Text);
                    }

                    Registry.LocalMachine.CreateSubKey(regPath, true);
                    Registry.LocalMachine.OpenSubKey(regPath, true).SetValue("Installed", "true", RegistryValueKind.String);
                    Registry.LocalMachine.OpenSubKey(regPath, true).SetValue("Version", version, RegistryValueKind.String);
                    Registry.LocalMachine.OpenSubKey(regPath, true).SetValue("Path", textBox_path.Text, RegistryValueKind.String);
                    if (radioButton_32.Checked == true)
                    {
                        Registry.LocalMachine.OpenSubKey(regPath, true).SetValue("Platform", "x32", RegistryValueKind.String);
                        System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Files64.zip");
                        ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Files32.zip", textBox_path.Text);
                        System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Files32.zip");
                    }
                    else if (radioButton_64.Checked == true)
                    {
                        Registry.LocalMachine.OpenSubKey(regPath, true).SetValue("Platform", "x64", RegistryValueKind.String);
                        System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Files32.zip");
                        ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Files64.zip", textBox_path.Text);
                        System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Files64.zip");
                    }

                    progressBar.Style = ProgressBarStyle.Blocks;
                }
                else // If installed
                {
                    string path = Registry.LocalMachine.OpenSubKey(regPath, true).GetValue("Path").ToString();
                    var listA = new List<byte>();
                    var listB = new List<byte>();

                    progressBar.Style = ProgressBarStyle.Marquee;

                    Registry.LocalMachine.CreateSubKey(regPath, true);
                    Registry.LocalMachine.OpenSubKey(regPath, true).SetValue("Version", version, RegistryValueKind.String);
                    if (radioButton_32.Checked == true)
                    {
                        Registry.LocalMachine.OpenSubKey(regPath, true).SetValue("Platform", "x32", RegistryValueKind.String);
                        System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Files64.zip");
                        ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Files32.zip", "Files32");
                        System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Files32.zip");
                        foreach (string file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"\Files32"))
                        {
                            string[] fileName = file.Split('\\');

                            if (System.IO.File.Exists(path + fileName[1]) == true)
                            {
                                listA.Clear();
                                listB.Clear();

                                foreach (byte byte1 in System.IO.File.ReadAllBytes(file))
                                {
                                    listA.Add(byte1);
                                }
                                foreach (byte byte2 in System.IO.File.ReadAllBytes(path + fileName[1]))
                                {
                                    listB.Add(byte2);
                                }

                                int loop = 0;
                                bool dif = false;
                                while (loop < listA.Count)
                                {
                                    if (listA[loop].ToString() != listB[loop].ToString())
                                    {
                                        dif = true;
                                    }
                                    loop++;
                                }
                                if (dif == true)
                                {
                                    System.IO.File.Delete(path + fileName[1]);
                                    System.IO.File.Move(file, path + fileName[1]);
                                }
                            }
                            else
                            {
                                System.IO.File.Move(file, path + fileName[1]);
                            }
                        }
                        System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Files32");
                    }
                    else if (radioButton_64.Checked == true)
                    {
                        Registry.LocalMachine.OpenSubKey(regPath, true).SetValue("Platform", "x64", RegistryValueKind.String);
                        System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Files32.zip");
                        ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Files64.zip", "Files64");
                        System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Files64.zip");
                        foreach (string file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "Files64"))
                        {
                            string[] fileName = file.Split('\\');

                            if (System.IO.File.Exists(path + fileName[1]) == true)
                            {
                                listA.Clear();
                                listB.Clear();

                                foreach (byte byte1 in System.IO.File.ReadAllBytes(file))
                                {
                                    listA.Add(byte1);
                                }
                                foreach (byte byte2 in System.IO.File.ReadAllBytes(path + fileName[1]))
                                {
                                    listB.Add(byte2);
                                }

                                int loop = 0;
                                bool dif = false;
                                while (loop < listA.Count)
                                {
                                    if (listA[loop].ToString() != listB[loop].ToString())
                                    {
                                        dif = true;
                                    }
                                    loop++;
                                }
                                if (dif == true)
                                {
                                    System.IO.File.Delete(path + fileName[1]);
                                    System.IO.File.Move(file, path + fileName[1]);
                                }
                            }
                            else
                            {
                                System.IO.File.Move(file, path + fileName[1]);
                            }
                        }
                        System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Files64");
                    }
                }

                progressBar.Style = ProgressBarStyle.Blocks;

                if (checkBox_startShortcut.Checked == true)
                {
                    AddStartMenuShortcut();
                }
                if (checkBox_desktopIcon.Checked == true)
                {
                    AddDesktopShortcut();
                }

                MessageBox.Show("Bronze Player (v" + version + ") has been installed!");
                Environment.Exit(0);
            }
            #region DE3UG
            catch (Exception exception)
            {
                progressBar.Style = ProgressBarStyle.Blocks;
                if (debug == true)
                {
                    var st = new StackTrace(exception, true);
                    var frame = st.GetFrame(0);
                    var line = frame.GetFileLineNumber();
                    MessageBox.Show(exception.Message.ToString() + "\n\n\nLinha: " + line.ToString(), "DE3UG - button_install_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }

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
            catch (Exception exception)
            {
                if (debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - radioButton_32_CheckedChanged()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }

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
            catch (Exception exception)
            {
                if (debug == true)
                {
                    MessageBox.Show(exception.ToString(), "DE3UG - radioButton_64_CheckedChanged()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }

        private void progressBar_Click(object sender, EventArgs e)
        {

            MessageBox.Show(Environment.Is64BitOperatingSystem.ToString());
        }
    }
}
