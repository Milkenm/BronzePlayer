#region Using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Installer;
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

                labelLink_version.Text = Installer.Installer.version;
                if (!String.IsNullOrEmpty(Installer.Installer.subVersion))
                {
                    labelLink_version.Text = labelLink_version.Text + "-" + Installer.Installer.subVersion;
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
