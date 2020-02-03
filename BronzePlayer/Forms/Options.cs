using System;
using System.Threading;
using System.Windows.Forms;

namespace BronzePlayer.Forms
{
    public partial class Options : Form
    {
        #region Refers

        // # ================================================================================================================================= #
        private Config config = new Config();

        private Lang lang = new Lang();
        private Main main = new Main(false, null);

        // # ================================================================================================================================= #

        #endregion Refers

        #region Vars

        // # ================================================================================================================================= #
        private bool initialize = false;

        // # ================================================================================================================================= #

        #endregion Vars

        #region Functions

        // # ================================================================================================================================= #
        private void LoadLangsCombobox()
        {
            var counter = 0;

            foreach (var value in lang.langsLong)
            {
                combobox_lang.Items.Add(value + " (" + lang.langs[counter] + ")");
                counter = counter + 1;
            }
        }

        // # ================================================================================================================================= #

        // # ================================================================================================================================= #
        private void LoadLang()
        {
            config.Reload();
            Thread.Sleep(1000);
            lang.Load(config.lang);

            // => Language
            groupbox_lang.Text = lang.lang_options__groupbox_lang;
            // => D3BUG
            checkbox_debug.Text = lang.lang_options__groupbox_debug_enabledebug;

            Refresh();
        }

        // # ================================================================================================================================= #

        #endregion Functions

        #region Load / Unload

        // # ================================================================================================================================= #
        public Options()
        {
            InitializeComponent();

            LoadLang();

            initialize = true;

            if (config.debug != false && config.debug != true)
            {
                config.debug = false;
                config.Save();
            }
            checkbox_debug.Checked = config.debug;

            LoadLangsCombobox();
            foreach (var item in combobox_lang.Items)
            {
                var item2 = item.ToString();

                item2 = item2.Replace(" (", null);
                item2 = item2.Replace(")", null);

                foreach (var value in lang.langsLong)
                {
                    item2 = item2.Replace(value, null);
                }

                if (item2 == config.lang)
                {
                    var index = combobox_lang.Items.IndexOf(item);
                    combobox_lang.SelectedIndex = index;
                }
            }
        }

        // # ================================================================================================================================= #

        #endregion Load / Unload

        #region Language

        // # ================================================================================================================================= #
        private void combobox_lang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initialize == true)
            {
                initialize = false;
            }
            else
            {
                var langCode = combobox_lang.Text;

                langCode = langCode.Replace(" (", null);
                langCode = langCode.Replace(")", null);

                foreach (var value in lang.langsLong)
                {
                    langCode = langCode.Replace(value, null);
                }

                config.lang = langCode;
                config.Save();
                LoadLang();

                new Main(false, null).LoadLang();
                new Main(false, null).Refresh();
                MessageBox.Show(lang.lang_options__msgbox_langchange_text, "Bronze Player", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // # ================================================================================================================================= #

        #endregion Language

        private void checkbox_debug_CheckedChanged(object sender, EventArgs e)
        {
            config.debug = checkbox_debug.Checked;
            config.Save();
        }

        private void Options_Load(object sender, EventArgs e)
        {
        }
    }
}