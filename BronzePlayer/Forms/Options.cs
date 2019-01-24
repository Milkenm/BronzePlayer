using System;
using System.Windows.Forms;

namespace BronzePlayer.Forms
{
    public partial class Options : Form
    {
        #region Refers
        // # ================================================================================================================================= #
        Config config = new Config();
        Lang lang = new Lang();
        Main main = new Main(false, null);
        // # ================================================================================================================================= #
        #endregion Refers



        #region Vars
        // # ================================================================================================================================= #
        bool initialize = false;
        // # ================================================================================================================================= #
        #endregion Vars



        #region Functions
        // # ================================================================================================================================= #
        void LoadLangsCombobox()
        {
            int counter = 0;

            foreach (string value in lang.langsLong)
            {
                combobox_lang.Items.Add(value + " (" + lang.langs[counter] + ")");
                counter = counter + 1;
            }
        }
        // # ================================================================================================================================= #
        #endregion Functions










        #region Load / Unload
        // # ================================================================================================================================= #
        public Options()
        {
            InitializeComponent();

            initialize = true;

            if (config.debug != false && config.debug != true)
            {
                config.debug = false;
                config.Save();
            }
            checkbox_debug.Checked = config.debug;

            LoadLangsCombobox();
            foreach (object item in combobox_lang.Items)
            {
                string item2 = item.ToString();

                item2 = item2.Replace(" (", null);
                item2 = item2.Replace(")", null);

                foreach (string value in lang.langsLong)
                {
                    item2 = item2.Replace(value, null);
                }

                if (item2 == config.lang)
                {
                    int index = combobox_lang.Items.IndexOf(item);
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
                string langCode = combobox_lang.Text;

                langCode = langCode.Replace(" (", null);
                langCode = langCode.Replace(")", null);

                foreach (string value in lang.langsLong)
                {
                    langCode = langCode.Replace(value, null);
                }

                config.lang = langCode;
                config.Save();

                new Main(false, null).LoadLang();
                //MessageBox.Show("You'll need to restart the program for the changes to be applied.", "Bronze Player", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        // # ================================================================================================================================= #
        #endregion Language

        private void checkbox_debug_CheckedChanged(object sender, EventArgs e)
        {
            config.debug = checkbox_debug.Checked;
            config.Save();
        }
    }
}
