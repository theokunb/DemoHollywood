using DemoHollywood.Services;
using DemoHollywood.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoHollywood.Views.AdminViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage(ServiceManager serviceManager)
        {
            InitializeComponent();
            BindingContext = new SettingsViewModel(serviceManager);
        }
    }
}