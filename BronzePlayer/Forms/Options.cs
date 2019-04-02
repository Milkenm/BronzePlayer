using System;
using System.Threading;
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



        // # ================================================================================================================================= #
        void LoadLang()
        {
            config.Reload();
            Thread.Sleep(1000);
            lang.Load(config.lang);

            // => Language
            groupbox_lang.Text = lang.lang_options__groupbox_lang;
            // => D3BUG
            checkbox_debug.Text = lang.lang_options__groupbox_debug_enabledebug;

            this.Refresh();
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



/*
 *  ╔═══════════════════════════════════════════════════════════════════════════════════════════╗
 *  ║▓▒░           THIS IS A LICENSE (or not, just something I typed in notepad)             ░▒▓║
 *   ║▓▒░                                                                                     ░▒▓║
 *    ║▓▒░                                                                                     ░▒▓║
 *     ║▓▒░This file has been stolen* from https://github.com/Milkenm/BronzePlayer              ░▒▓║
 *     ║▓▒░                                                                                     ░▒▓║
 *    ║▓▒░ This file can be used, because it's a file, and you can share it,                   ░▒▓║
 *   ║▓▒░    and if you keep this little message, you will make me happy.                     ░▒▓║
 *  ║▓▒░     Please don't remove it =) It even has this cute map-shaped box and bad english! ░▒▓║
 *  ║▓▒░   If you received a copy of this file, and can see this message, congrats,          ░▒▓║
 *  ║▓▒░     the person that gave you this file is a nice human!                             ░▒▓║
 *   ║▓▒░                                                                                     ░▒▓║
 *    ║▓▒░                                                                                     ░▒▓║
 *     ║▓▒░    *jk, this file was not stolen, chill. - or was it?                               ░▒▓║
 *     ║▓▒░                                                                                     ░▒▓║
 *     ║▓▒░                                                                                     ░▒▓║
 *    ║▓▒░                                                          Typed by: Milkenm          ░▒▓║
 *   ║▓▒░                                                                                     ░▒▓║
 *  ╚═══════════════════════════════════════════════════════════════════════════════════════════╝
*/
