﻿#region Using
using System;
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
    public string lang_main__menu_favorites_add { get; set; }
    public string lang_main__menu_other { get; private set; }
    public string lang_main__menu_other_options { get; private set; }
    public string lang_main__menu_other_about { get; set; }
    public string lang_main__contextmenustrip_remove { get; private set; }
    public string lang_main__msgbox_ytsuccess_text { get; private set; }
    public string lang_main__msgbox_ytfail_text { get; private set; }
    public string lang_main__msgbox_ytnodirectory_text { get; private set; }
    public string lang_main__label_ytdownloading { get; set; }

    public string lang_options__groupbox_lang { get; private set; }
    public string lang_options__groupbox_debug_enabledebug { get; private set; }
    public string lang_options__msgbox_langchange_text { get; private set; }

    public string lang_about__label_authors { get; set; }
    public string lang_about__label_contributors { get; set; }
    public string lang_about__label_version { get; set; }
    #endregion Get / Set



    public void Load(string _local)
    {
        try
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
                lang_main__menu_favorites_add = "Adicionar";
                lang_main__menu_other = "Outros";
                lang_main__menu_other_options = "Configurações";
                lang_main__menu_other_about = "Sobre";
                lang_main__contextmenustrip_remove = "Remover";
                lang_main__msgbox_ytsuccess_text = "Download completo!";
                lang_main__msgbox_ytfail_text = "Algo de errado aconteceu durante o download...";
                lang_main__msgbox_ytnodirectory_text = "Tens de escolher uma pasta para o download!";
                lang_main__label_ytdownloading = "A transferir...";

                lang_options__groupbox_lang = "Idioma:";
                lang_options__groupbox_debug_enabledebug = "Mostrar erros.";
                lang_options__msgbox_langchange_text = "Provavelmente, vais ter de fechar a voltar a abrir a aplicação para as alterações serem aplicadas.";

                lang_about__label_authors = "Autores: ";
                lang_about__label_contributors = "Contribuidores: ";
                lang_about__label_version = "Versão: ";
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
                lang_main__menu_favorites_add = "Add";
                lang_main__menu_other = "Other";
                lang_main__menu_other_options = "Options";
                lang_main__menu_other_about = "About";
                lang_main__contextmenustrip_remove = "Remove";
                lang_main__msgbox_ytsuccess_text = "Download complete!";
                lang_main__msgbox_ytfail_text = "Something went wrong during the download...";
                lang_main__msgbox_ytnodirectory_text = "You need to choose a folder for the download!";
                lang_main__label_ytdownloading = "Downloading...";

                lang_options__groupbox_lang = "Language:";
                lang_options__groupbox_debug_enabledebug = "Show errors.";
                lang_options__msgbox_langchange_text = "Probably, you'll have to restart the application for changes to be applied.";

                lang_about__label_authors = "Authors: ";
                lang_about__label_contributors = "Contributors: ";
                lang_about__label_version = "Version: ";
            }
            #endregion en-EN
        }
        catch (Exception exception)
        {
            Scripts.tools.Exception(exception);
        }
    }
}
