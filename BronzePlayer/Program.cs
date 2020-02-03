using System;
using System.IO;
using System.Windows.Forms;

namespace BronzePlayer
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] _args)
        {
            // => If the program was open by files:
            if (_args != null && _args.Length > 0)
            {
                var fileName = _args[0];

                if (File.Exists(fileName)) // => If the file[s] exist[s]:
                {
                    // => Does stuff ,_,
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Application.Run(new Main(true, fileName)); // => Shows the main form.
                }
                else // => If the file[s] do[es] not exist:
                {
                    MessageBox.Show("The file does not exist!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); // => Shows a message saying that the file[s] do not exist.
                }
            }
            else // => If the program was not open by files:
            {
                // => Runs normally.
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main(false, null));
            }
        }
    }
}
