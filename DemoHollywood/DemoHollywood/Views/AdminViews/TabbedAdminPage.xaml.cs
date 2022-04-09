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
        public TabbedAdminPage(FireBaseAuth fireBaseAuth, RealTimeDB realTimeDB)
        {
            InitializeComponent();
            this.fireBaseAuth = fireBaseAuth;
            this.realTimeDB = realTimeDB;
            isLoaded = false;

        }

        private readonly FireBaseAuth fireBaseAuth;
        private readonly RealTimeDB realTimeDB;
        private bool isLoaded;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Preferences.Set(Strings.PermissionToken, true);

            if (!isLoaded)
            {
                Children.Add(new PageBrowseAppointments(fireBaseAuth, realTimeDB));
                Children.Add(new StuffPage(fireBaseAuth, realTimeDB));
                isLoaded = true;
            }
        }
    }
}