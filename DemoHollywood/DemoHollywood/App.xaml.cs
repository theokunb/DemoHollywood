using DemoHollywood.Helpers;
using DemoHollywood.Services;
using DemoHollywood.Views;
using DemoHollywood.Views.AdminViews;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DemoHollywood
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            fireBaseAuth = new FireBaseAuth(authWebKey);
            realTimeDB = new RealTimeDB(realTimeDatabaseToken);
        }

        private const string realTimeDatabaseToken = "https://demohollywood-f65be-default-rtdb.europe-west1.firebasedatabase.app/";
        private const string authWebKey = "AIzaSyCVG2XXBnOoBBqITk71JwA8BFCEjeiaFzA";
        private readonly FireBaseAuth fireBaseAuth;
        private readonly RealTimeDB realTimeDB;

        protected override void OnStart()
        {
            if (string.IsNullOrEmpty(Preferences.Get(Strings.AuthToken, string.Empty)))
                MainPage = new LoginPage(fireBaseAuth, realTimeDB);
            else
            {
                if (Preferences.Get(Strings.PermissionToken, false))
                    MainPage = new TabbedAdminPage(fireBaseAuth, realTimeDB);
                else
                    MainPage = new TabbedMainPage(fireBaseAuth, realTimeDB);
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
