using Acr.UserDialogs;
using DemoHollywood.Helpers;
using DemoHollywood.Services;
using Firebase.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoHollywood.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedMainPage : TabbedPage
    {

        public TabbedMainPage(ServiceManager serviceManager)
        {
            InitializeComponent();
            this.serviceManager = serviceManager;
            pageLoaded = false;
        }

        private readonly ServiceManager serviceManager;
        
        private bool pageLoaded;
        private User currentUser;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Preferences.Set(Strings.PermissionToken, false);
            if (!pageLoaded)
            {
                UserDialogs.Instance.ShowLoading("Идет подключение...");

                currentUser = (await serviceManager.FireBaseAuth.GetProfileInfo()).User;

                Children.Add(new RegistrationPage(currentUser, serviceManager.RealTimeDB));
                Children.Add(new ProfilePage(currentUser, serviceManager));
                Children.Add(new NewsPage(serviceManager));
                Children.Add(new AboutUsPage(currentUser, serviceManager));

                pageLoaded = true;
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}