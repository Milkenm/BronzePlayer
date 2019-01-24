using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class Lang
{
    #region Vars
    public List<string> langs = new List<string> { "pt_PT", "en_EN" };
    public List<string> langsLong = new List<string> { "Português", "English" };
    #endregion Vars


    #region Get / Set
    public string lang_main__button_ytdownload { get; private set; }
    public string lang_main__checkbox_loop { get; private set; }
    public string lang_main__menu_file { get; private set; }
    public string lang_main__menu_file_open { get; private set; }
    public string lang_main__menu_file_exit { get; private set; }
    public string lang_main__menu_debug_opentestingground { get; private set; }
    public string lang_main__menu_other { get; private set; }
    public string lang_main__menu_other_options { get; private set; }
    public string lang_main__contextmenustrip_remove { get; private set; }
    public string lang_main__msgbox_ytsuccess_text { get; private set; }
    public string lang_main__msgbox_ytfail_text { get; private set; }
    public string lang_main__msgbox_ytnodirectory_text { get; private set; }

    public string lang_options__groupbox_lang { get; private set; }
    public string lang_options__groupbox_debug_enabledebug { get; private set; }
    #endregion Get / Set



    public void Load(string _local)
    {
        #region pt-PT
        if (_local == "pt_PT")
        {
            lang_main__button_ytdownload = "Download";
            lang_main__checkbox_loop = "Repetir";
            lang_main__menu_file = "Ficheiro";
            lang_main__menu_file_open = "Abrir...";
            lang_main__menu_file_exit = "Fechar";
            lang_main__menu_debug_opentestingground = "Abrir o TestingGround";
            lang_main__menu_other = "Outros";
            lang_main__menu_other_options = "Configurações";
            lang_main__contextmenustrip_remove = "Remover";
            lang_main__msgbox_ytsuccess_text = "Download completo!";
            lang_main__msgbox_ytfail_text = "Algo de errado aconteceu durante o download...";
            lang_main__msgbox_ytnodirectory_text = "Tens de escolher uma pasta para o download!";

            lang_options__groupbox_lang = "Idioma:";
            lang_options__groupbox_debug_enabledebug = "Ativar modo DE3UG.";
        }
        #endregion pt-PT

        #region en-EN
        else if (_local == "en_EN")
        {
            lang_main__button_ytdownload = "Download";
            lang_main__checkbox_loop = "Loop";
            lang_main__menu_file = "File";
            lang_main__menu_file_open = "Open...";
            lang_main__menu_file_exit = "Exit";
            lang_main__menu_debug_opentestingground = "Open TestingGround";
            lang_main__menu_other = "Other";
            lang_main__menu_other_options = "Options";
            lang_main__contextmenustrip_remove = "Remove";
            lang_main__msgbox_ytsuccess_text = "Download complete!";
            lang_main__msgbox_ytfail_text = "Something went wrong during the download...";
            lang_main__msgbox_ytnodirectory_text = "You need to choose a folder for the download!";

            lang_options__groupbox_lang = "Language:";
            lang_options__groupbox_debug_enabledebug = "Enable DE3UG mode.";
        }
        #endregion en-EN
    }
}