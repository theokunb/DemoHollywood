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
            FireBaseAuth fireBaseAuth = new FireBaseAuth(authWebKey);
            RealTimeDB realTimeDB = new RealTimeDB(realTimeDatabaseToken);
            VkClient vkClient = new VkClient();
            Storage storage = new Storage(firebaseStorage);

            serviceManager = new ServiceManager(realTimeDB, fireBaseAuth, vkClient, storage);
        }

        private const string realTimeDatabaseToken = "https://demohollywood-f65be-default-rtdb.europe-west1.firebasedatabase.app/";
        private const string authWebKey = "AIzaSyCVG2XXBnOoBBqITk71JwA8BFCEjeiaFzA";
        private const string firebaseStorage = "demohollywood-f65be.appspot.com";
        

        private readonly ServiceManager serviceManager;



        protected override void OnStart()
        {
            if (string.IsNullOrEmpty(Preferences.Get(Strings.AuthToken, string.Empty)))
                MainPage = new LoginPage(serviceManager);
            else
            {
                if (Preferences.Get(Strings.PermissionToken, false))
                    MainPage = new TabbedAdminPage(serviceManager);
                else
                    MainPage = new TabbedMainPage(serviceManager);
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
