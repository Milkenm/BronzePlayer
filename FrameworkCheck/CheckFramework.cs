﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace FrameworkCheck
{
    public partial class CheckFramework : Form
    {
        public CheckFramework()
        {
            try
            {
                if (Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full") != null)
                {
                    int key = Convert.ToInt32(Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\").GetValue("Release"));

                    if (key >= 461808) {}
                    else
                    {
                        var process = Process.Start("Framework.exe");
                        process.WaitForExit();
                    }
                }
                else
                {
                    var process = Process.Start("Framework.exe");
                    process.WaitForExit();
                }
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Environment.Exit(0);
            }
        }
    }
}



/*
 *  ╔═══════════════════════════════════════════════════════════════════════════════════════════╗
 *  ║▓▒░           THIS IS A LICENSE (or not, just something I typed in notepad)             ░▒▓║
 *   ║▓▒░                                                                                     ░▒▓║
 *    ║▓▒░                                                                                     ░▒▓║
 *     ║▓▒░This file has been stolen* from https://github.com/Milkenm/BronzePlayer              ░▒▓║
 *     ║▓▒░                                                                                     ░▒▓║
 *    ║▓▒░ This file can be used, because it's a file, and you can share it,                   ░▒▓║
 *   ║▓▒░    and if you keep this little message, you will make me happy.                     ░▒▓║
 *  ║▓▒░     Please don't remove it =) It even has this cute map-shaped box and bad english! ░▒▓║
 *  ║▓▒░   If you received a copy of this file, and can see this message, congrats,          ░▒▓║
 *  ║▓▒░     the person that gave you this file is a nice human!                             ░▒▓║
 *   ║▓▒░                                                                                     ░▒▓║
 *    ║▓▒░                                                                                     ░▒▓║
 *     ║▓▒░    *jk, this file was not stolen, chill. - or was it?                               ░▒▓║
 *     ║▓▒░                                                                                     ░▒▓║
 *     ║▓▒░                                                                                     ░▒▓║
 *    ║▓▒░                                                          Typed by: Milkenm          ░▒▓║
 *   ║▓▒░                                                                                     ░▒▓║
 *  ╚═══════════════════════════════════════════════════════════════════════════════════════════╝
*/
