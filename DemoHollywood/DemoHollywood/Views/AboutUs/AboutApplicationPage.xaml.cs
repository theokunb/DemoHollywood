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
    public partial class AboutApplicationPage : ContentPage
    {
        public AboutApplicationPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public string Version => $"Сборка {Xamarin.Essentials.AppInfo.Version}";

        private async void ImageBackTapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}