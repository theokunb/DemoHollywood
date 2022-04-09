using DemoHollywood.Services;
using DemoHollywood.ViewModels;
using Firebase.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoHollywood.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage(User currentUser,FireBaseAuth fireBaseAuth, RealTimeDB realTimeDB)
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel(currentUser, fireBaseAuth, realTimeDB);
        }
    }
}