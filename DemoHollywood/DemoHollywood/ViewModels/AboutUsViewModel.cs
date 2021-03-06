using DemoHollywood.Helpers;
using DemoHollywood.Services;
using DemoHollywood.Views;
using Firebase.Auth;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DemoHollywood.ViewModels
{
    public class AboutUsViewModel : BaseViewModel
    {
        public AboutUsViewModel(User user,ServiceManager serviceManager)
        {
            this.user = user;
            this.serviceManager = serviceManager;
            CommandButtonTapped = new Command((param) => ButtonTapped(param));
            CommandCallTapped = new Command((param) => CallTapped(param));
            CommandWriteTapped = new Command((param) => WriteTapped(param));
            CommandRedirectToUrl = new Command((param) => RedirectToUrl(param));
        }



        private readonly User user;
        private readonly ServiceManager serviceManager;

        public string UrlVk => Strings.UrlVk;
        public string UrlTelegram => Strings.UrlTelegram;
        public string UrlSite => Strings.UrlSite;
        public string PhoneNumber => Strings.PhoneNumber;
        public string ParamMap => Strings.Map;
        public string ParamDocuments => Strings.Documents;
        public string ParamAbout => Strings.AboutApplication;

        public ICommand CommandButtonTapped { get; }
        public ICommand CommandCallTapped { get; }
        public ICommand CommandWriteTapped { get; }
        public ICommand CommandRedirectToUrl { get; }


        private async void ButtonTapped(object param)
        {
            if (param.ToString().Equals(ParamMap))
                await Application.Current.MainPage.Navigation.PushModalAsync(new MapPage());
            else if (param.ToString().Equals(ParamDocuments))
                await Application.Current.MainPage.Navigation.PushModalAsync(new DocumentsPage(serviceManager));
            else if (param.ToString().Equals(ParamAbout))
                await Application.Current.MainPage.Navigation.PushModalAsync(new AboutApplicationPage());
        }
        private void CallTapped(object param)
        {
            PhoneDialer.Open(param.ToString());
        }
        private async void WriteTapped(object param)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new ChatPage(user, serviceManager.RealTimeDB));
        }
        private async void RedirectToUrl(object param)
        {
            await Launcher.OpenAsync(param.ToString());
        }
    }
}
