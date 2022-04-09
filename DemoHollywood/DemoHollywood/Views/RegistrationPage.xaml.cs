using DemoHollywood.Services;
using DemoHollywood.ViewModels;
using Firebase.Auth;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoHollywood.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage(User currentUser, RealTimeDB realTimeDB)
        {
            InitializeComponent();
            BindingContext = new RegistrationViewModel(currentUser, realTimeDB);
        }

        private void ImageDatePickerTapped(object sender, EventArgs e)
        {
            appointDatePicker.Focus();
        }
    }
}