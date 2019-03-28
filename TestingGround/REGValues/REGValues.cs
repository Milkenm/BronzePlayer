using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace TestingGround.REGValues
{
    public partial class REGValues : Form
    {
        public REGValues()
        {
            InitializeComponent();
        }

        private void button_get_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/C REG QUERY HKLM\\Software\\Milkenm\\BronzePlayer /V Version";
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            MessageBox.Show(output);
        }
    }
}
