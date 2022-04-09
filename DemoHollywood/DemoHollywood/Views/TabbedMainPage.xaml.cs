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

        public TabbedMainPage(FireBaseAuth fireBaseAuth, RealTimeDB realTimeDB)
        {
            InitializeComponent();
            this.fireBaseAuth = fireBaseAuth;
            this.realTimeDB = realTimeDB;
            pageLoaded = false;
        }

        private readonly FireBaseAuth fireBaseAuth;
        private readonly RealTimeDB realTimeDB;
        
        private bool pageLoaded;
        private User currentUser;

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Preferences.Set(Strings.PermissionToken, false);
            if(!pageLoaded)
            {
                UserDialogs.Instance.ShowLoading("Идет подключение...");
                currentUser = (await fireBaseAuth.GetProfileInfo()).User;
                Children.Add(new RegistrationPage(currentUser, realTimeDB));
                Children.Add(new ProfilePage(currentUser, fireBaseAuth, realTimeDB));
                Children.Add(new NewsPage());
                Children.Add(new AboutUsPage(currentUser, realTimeDB));
                pageLoaded = true;
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}