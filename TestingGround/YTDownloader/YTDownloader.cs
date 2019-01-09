using System;
using MediaToolkit;
using MediaToolkit.Model;
using System.IO;
using System.Windows.Forms;
using VideoLibrary;

namespace TestingGround
{
    public partial class YTDownloader : Form
    {
        public YTDownloader()
        {
            InitializeComponent();
        }

        private void button_download_Click(object sender, EventArgs e)
        {
            Download();
        }

        void Download()
        {
            var outputdir = @"C:\Users\ASUS\Desktop\";
            var youtube = YouTube.Default;
            var vid = youtube.GetVideo(textbox_url.Text);
            File.WriteAllBytes(outputdir + vid.FullName, vid.GetBytes());

            var inputfile = new MediaFile { Filename = outputdir + vid.FullName };
            var outputfile = new MediaFile { Filename = $"{outputdir + vid.FullName}.mp3" };

            using (var engine = new Engine())
            {
                engine.GetMetadata(inputfile);

                engine.Convert(inputfile, outputfile);
            }
        }
    }
}
