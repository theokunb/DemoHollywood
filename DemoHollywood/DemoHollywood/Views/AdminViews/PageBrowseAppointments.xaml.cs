using DemoHollywood.Services;
using DemoHollywood.ViewModels.AdminViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoHollywood.Views.AdminViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageBrowseAppointments : ContentPage
    {
        public PageBrowseAppointments(ServiceManager serviceManager)
        {
            InitializeComponent();
            BindingContext = new BrowseAppointViewModel(serviceManager);
        }
    }
}