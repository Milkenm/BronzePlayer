using System;
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
