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
