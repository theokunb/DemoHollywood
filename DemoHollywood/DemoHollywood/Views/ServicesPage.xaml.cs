using DemoHollywood.Models;
using DemoHollywood.Services;
using DemoHollywood.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoHollywood.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServicesPage : ContentPage
    {
        public ServicesPage(RealTimeDB realTimeDB, ReueqstAppointments request)
        {
            InitializeComponent();
            BindingContext = new ServicesViewModel(realTimeDB, request);
        }

        
    }
}