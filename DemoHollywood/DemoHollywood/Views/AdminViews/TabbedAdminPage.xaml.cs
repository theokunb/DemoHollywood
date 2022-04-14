using DemoHollywood.Helpers;
using DemoHollywood.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoHollywood.Views.AdminViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedAdminPage : TabbedPage
    {
        public TabbedAdminPage(ServiceManager serviceManager)
        {
            InitializeComponent();
            this.serviceManager = serviceManager;
            isLoaded = false;

        }

        private readonly ServiceManager serviceManager;
        private bool isLoaded;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Preferences.Set(Strings.PermissionToken, true);

            if (!isLoaded)
            {
                Children.Add(new PageBrowseAppointments(serviceManager));
                Children.Add(new StuffPage(serviceManager));
                Children.Add(new SettingsPage(serviceManager));
                isLoaded = true;
            }
        }
    }
}