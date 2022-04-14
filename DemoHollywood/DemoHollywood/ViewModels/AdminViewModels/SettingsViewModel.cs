using DemoHollywood.Services;
using DemoHollywood.Views.AdminViews;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoHollywood.ViewModels.AdminViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel(ServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;

            CommandDocuments = new Command(param => ButtonDocuments(param));
        }

        private readonly ServiceManager serviceManager;

        public ICommand CommandDocuments { get; }



        private async void ButtonDocuments(object param)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new DocumentsPage(serviceManager));
        }
    }
}
