using DemoHollywood.Services;
using DemoHollywood.ViewModels;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoHollywood.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutUsPage : ContentPage
    {
        public AboutUsPage(User user, ServiceManager serviceManager)
        {
            InitializeComponent();
            BindingContext = new AboutUsViewModel(user, serviceManager);
        }
    }
}