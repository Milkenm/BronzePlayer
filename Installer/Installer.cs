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
        public static readonly string version = "0.3.0", subVersion = "alpha";
        string regPath, regPath64 = @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Milkenm\Bronze Player", regPath32 = @"HKEY_LOCAL_MACHINE\SOFTWARE\Milkenm\Bronze Player";
        string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Milkenm\Bronze Player\";
        bool install = false, newerInstalled = false;
        bool x64 = Environment.Is64BitOperatingSystem;
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

                label_version.Text = "v." + version;
                if (!String.IsNullOrEmpty(subVersion))
                {
                    label_version.Text = label_version.Text + "-" + subVersion;
                }

                this.Enabled = false;
                if (x64 == true)
                {
                    regPath = regPath64;
                }
                else
                {
                    regPath = regPath32;
                }
                if (Registry.GetValue(regPath, "Installed", null) != null)
                {
                    string[] installedVersion = Registry.GetValue(regPath, "Version", true).ToString().Split('.');
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
                        MessageBox.Show("A newer version (" + Registry.GetValue(regPath, "Version", true).ToString() + ") is installed!", "Bronze Player", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    else if (install == false)
                    {
                        MessageBox.Show("This version (" + version + ") is already installed!", "Bronze Player", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }

                    textBox_path.Text = Registry.GetValue(regPath, "Path", null).ToString();
                    textBox_path.ReadOnly = true;
                }
                else
                {
                    #region Get TextBox Directory
                    if (x64 == true)
                    {
                        textBox_path.Text = Environment.GetEnvironmentVariable("ProgramFiles(x86)") + @"\Milkenm\Bronze Player\";
                    }
                    else
                    {
                        textBox_path.Text = Environment.GetEnvironmentVariable("ProgramFiles") + @"\Milkenm\Bronze Player\";
                    }
                    #endregion Get TextBox Directory
                }
                this.Enabled = true;
            }
            #region DE3UG
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
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
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
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
                if (Registry.GetValue(regPath, "Installed", null) == null)
                {
                    if (Directory.Exists(textBox_path.Text) == false)
                    {
                        Directory.CreateDirectory(textBox_path.Text);
                    }
                    
                    Registry.SetValue(regPath, "Installed", true);
                    Registry.SetValue(regPath, "Path", textBox_path.Text);
                    Registry.SetValue(regPath, "Platform", "x32");
                    ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Files32.zip", textBox_path.Text);
                    System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Files32.zip");

                    progressBar.Style = ProgressBarStyle.Blocks;
                }
                #endregion Fresh Install
                #region Update Install
                else // If installed
                {
                    string path = Registry.GetValue(regPath, "Path", null).ToString();

                    #region Remove Old Installation Files
                    foreach (var filePath in Directory.GetFiles(path))
                    {
                        if (Path.GetFileNameWithoutExtension(filePath) != "BronzePlayer.exe")
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }
                    Thread.Sleep(2000);
                    #endregion Remove Old Installation Files

                    #region Update
                    ZipFile.ExtractToDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Files32.zip", AppDomain.CurrentDomain.BaseDirectory + @"\Files32");

                    foreach (string file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"\Files32"))
                    {
                        string fileName = Path.GetFileName(file);
                        #region Update Database
                        if (fileName == "BD.mdb")
                        {
                            if (System.IO.File.Exists(appData + fileName))
                            {
                                System.IO.File.Delete(appData + fileName);
                            }
                            else if (!Directory.Exists(appData + fileName))
                            {
                                Directory.CreateDirectory(appData);
                            }
                            System.IO.File.Copy(file, appData + fileName);
                        }
                        #endregion Update Database
                        if (System.IO.File.Exists(path + fileName) == false)
                        {
                            System.IO.File.Delete(path + fileName);
                            System.IO.File.Copy(file, path + fileName);
                        }
                    }

                    Directory.Delete(AppDomain.CurrentDomain.BaseDirectory + @"\Files32", true);
                    #endregion Update
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
                
                Registry.SetValue(regPath, "Version", version);
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
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        // # ================================================================================================================================= #
        #endregion Install Button
    }
}



/*
 *  ╔═══════════════════════════════════════════════════════════════════════════════════════════╗
 *  ║▓▒░           # THE MAP License | 1.0 | Copyright © 2019 Milkenm                        ░▒▓║
 *    ║▓▒░                                                                                     ░▒▓║
 *     ║▓▒░                                                                                     ░▒▓║
 *      ║▓▒░   This file has been stolen* from https://github.com/Milkenm/BronzePlayer           ░▒▓║
 *       ║▓▒░  If you received a copy of this file, and can see this message, congrats,           ░▒▓║
 *        ║▓▒░     the person that gave you this file is a nice human!                             ░▒▓║
 *        ║▓▒░ Everyone is allowed to copy and distribute verbatim copies of this license document,░▒▓║
 *        ║▓▒░     but changing it is definitly not allowed.                                       ░▒▓║
 *       ║▓▒░                                                                                     ░▒▓║
 *      ║▓▒░                                                                                     ░▒▓║
 *     ║▓▒░                                                                                     ░▒▓║
 *    ║▓▒░                                                                                     ░▒▓║
 *   ║▓▒░ > TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION                   ░▒▓║
 *  ║▓▒░                                                                                     ░▒▓║
 *  ║▓▒░  1. The stolen file can be used, because it's a file, and you can edit/share it,    ░▒▓║
 *  ║▓▒░         as long as you keep this license file.                                      ░▒▓║
 *  ║▓▒░  2. Don't delete this license (I made it look like a map so you won't delete it).   ░▒▓║
 *   ║▓▒░ 3. No, you cannot reshape the map.                                                  ░▒▓║
 *    ║▓▒░                                                                                     ░▒▓║
 *     ║▓▒░                                                                                     ░▒▓║
 *      ║▓▒░                                                                                     ░▒▓║
 *      ║▓▒░    *jk, this file was not stolen, chill. - or was it?                               ░▒▓║
 *      ║▓▒░                                                                                     ░▒▓║
 *     ║▓▒░                                                             Typed by: Milkenm       ░▒▓║
 *    ║▓▒░                                                                                     ░▒▓║
 *   ╚═══════════════════════════════════════════════════════════════════════════════════════════╝
 */
