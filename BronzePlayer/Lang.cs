#region Using
using System.Collections.Generic;
#endregion Using

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
    public string lang_main__menu_favorites { get; private set; }
    public string lang_main__menu_other { get; private set; }
    public string lang_main__menu_other_options { get; private set; }
    public string lang_main__contextmenustrip_remove { get; private set; }
    public string lang_main__msgbox_ytsuccess_text { get; private set; }
    public string lang_main__msgbox_ytfail_text { get; private set; }
    public string lang_main__msgbox_ytnodirectory_text { get; private set; }
    public string lang_main__label_ytdownloading { get; set; }

    public string lang_options__groupbox_lang { get; private set; }
    public string lang_options__groupbox_debug_enabledebug { get; private set; }
    public string lang_options__msgbox_langchange_text { get; private set; }
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
            lang_main__menu_favorites = "Favoritos";
            lang_main__menu_other = "Outros";
            lang_main__menu_other_options = "Configurações";
            lang_main__contextmenustrip_remove = "Remover";
            lang_main__msgbox_ytsuccess_text = "Download completo!";
            lang_main__msgbox_ytfail_text = "Algo de errado aconteceu durante o download...";
            lang_main__msgbox_ytnodirectory_text = "Tens de escolher uma pasta para o download!";
            lang_main__label_ytdownloading = "A transferir...";

            lang_options__groupbox_lang = "Idioma:";
            lang_options__groupbox_debug_enabledebug = "Mostrar erros.";
            lang_options__msgbox_langchange_text = "Provavelmente, vais ter de fechar a voltar a abrir a aplicação para as alterações serem aplicadas.";
        }
        #endregion pt-PT

        #region en-EN
        else if (_local == "en_EN")
        {
            lang_main__button_ytdownload = "Download";
            lang_main__checkbox_loop = "Loop";
            lang_main__menu_file = "File";
            lang_main__menu_file_open = "Open...";
            lang_main__menu_file_exit = "Close";
            lang_main__menu_favorites = "Favorites";
            lang_main__menu_other = "Other";
            lang_main__menu_other_options = "Options";
            lang_main__contextmenustrip_remove = "Remove";
            lang_main__msgbox_ytsuccess_text = "Download complete!";
            lang_main__msgbox_ytfail_text = "Something went wrong during the download...";
            lang_main__msgbox_ytnodirectory_text = "You need to choose a folder for the download!";
            lang_main__label_ytdownloading = "Downloading...";

            lang_options__groupbox_lang = "Language:";
            lang_options__groupbox_debug_enabledebug = "Show errors.";
            lang_options__msgbox_langchange_text = "Probably, you'll have to restart the application for changes to be applied.";
        }
        #endregion en-EN
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
