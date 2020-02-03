using System;
using System.IO;

namespace BronzePlayer
{
	internal static class Program
	{
		[STAThread]
		private static void Main(string[] _args)
		{
			// Check if the program was started using "Open with"
			if (_args != null && _args.Length > 0)
			{
				var fileName = _args[0];

				// Check if the file exists
				if (File.Exists(fileName))
				{
					// Runs using the provided files
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new Main(true, fileName));
				}
				else
				{
					MessageBox.Show("The file does not exist!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error); // => Shows a message saying that the file[s] do not exist.
				}
			}
			else
			{
				// Runs normally
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new Main(false, null));
			}
		}
	}
}
