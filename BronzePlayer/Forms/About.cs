#region Using
using System;
using System.Drawing;
using System.Windows.Forms;
#endregion Using

namespace BronzePlayer.Forms
{
    public partial class About : Form
    {
        #region Functions
        void LoadLang()
        {
            var lang = new Lang();
            label_authors.Text = lang.lang_about__label_authors;
            label_contributors.Text = lang.lang_about__label_contributors;
            label_version.Text = lang.lang_about__label_version;
        }
        #endregion Functions








        public About(Point _location)
        {
            try
            {
                InitializeComponent();
                _location.X = _location.X + 16;
                _location.Y = _location.Y + 59;
                this.Location = _location;

                labelLink_authors.Links.Add(0, 7, "https://www.github.com/Milkenm");
                labelLink_authors.LinkClicked += LabelLink_authors_LinkClicked;

                Main main = new Main(false, null);

                labelLink_version.Text = main.version;
                if (!String.IsNullOrEmpty(main.subVersion))
                {
                    labelLink_version.Text = labelLink_version.Text + "-" + main.subVersion;
                }
            }
            #region DE3UG
            catch (Exception exception)
            {
                Scripts.tools.Exception(exception);
            }
            #endregion DE3UG
        }

        private void LabelLink_authors_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            #region DE3UG
            catch (Exception exception)
            {
                Scripts.tools.Exception(exception);
            }
            #endregion DE3UG
        }

        private void labelLink_version_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.github.com/Milkenm/BronzePlayer/releases");
        }
    }
}
