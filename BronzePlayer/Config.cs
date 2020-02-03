using System.ComponentModel;
using System.Configuration;

namespace BronzePlayer
{
    // Esta classe permite que você trate eventos específicos na classe de configurações:
    //  O evento SettingChanging é gerado antes da alteração de um valor de configuração.
    //  O evento PropertyChanged é gerado depois da alteração de um valor de configuração.
    //  O evento SettingsLoaded é gerado depois do carregamento dos valores de configuração.
    //  O evento SettingsSaving é gerado antes de salvar os valores de configuração.
    public sealed partial class Config
    {
        public Config()
        {
            // // Para adicionar manipuladores de eventos para salvar e alterar configurações, remova os comentários das linhas abaixo:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }

        private void SettingChangingEventHandler(object sender, SettingChangingEventArgs e)
        {
            // Adicione código para manipular o evento SettingChangingEvent aqui.
        }

        private void SettingsSavingEventHandler(object sender, CancelEventArgs e)
        {
            // Adicione código para manipular o evento SettingsSaving aqui.
        }
    }
}
