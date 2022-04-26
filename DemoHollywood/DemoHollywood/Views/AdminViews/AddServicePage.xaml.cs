using DemoHollywood.Services;
using DemoHollywood.ViewModels.AdminViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoHollywood.Views.AdminViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddServicePage : ContentPage
    {
        public AddServicePage(ServiceManager serviceManager)
        {
            InitializeComponent();
            BindingContext = new AddServiceViewModel(serviceManager);
        }
    }
}