using DemoHollywood.Models;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DemoHollywood.Services;
using System.Collections.Generic;
using DemoHollywood.Views.AdminViews;
using DemoHollywood.Helpers;
using System.Linq;
using Xamarin.Essentials;
using Acr.UserDialogs;

namespace DemoHollywood.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage, INotifyPropertyChanged
    {
        public LoginPage(ServiceManager serviceManager)
        {
            InitializeComponent();

            BindingContext = this;
            this.serviceManager = serviceManager;
            fireBaseAuth = serviceManager.FireBaseAuth;
            realTimeDB = serviceManager.RealTimeDB;


            pages = new Dictionary<bool, Page>
            {
                { false, new TabbedMainPage(serviceManager) },
                { true, new TabbedAdminPage(serviceManager) }
            };

        }

        private bool isAdmin;
        private readonly FireBaseAuth fireBaseAuth;
        private readonly RealTimeDB realTimeDB;
        private readonly ServiceManager serviceManager;
        private string email;
        private string password;
        private Dictionary<bool, Page> pages;


        public bool IsAdmin
        {
            get => isAdmin;
            set
            {
                isAdmin = value;
                OnPropertyChanged();
            }
        }
        public string GreetingsText
        {
            get
            {
                if (IsAdmin)
                    return Strings.GreetingsDoctor;
                else
                    return Strings.GreetingsPatient;
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }
        public ICommand CommandSignIn { get; }



        private async void ButtonLoginClicked(object sender, System.EventArgs e)
        {
            UserDialogs.Instance.ShowLoading("Загрузка...");
            var isAuthSuccess = await fireBaseAuth.SignIn(Email, Password);
            if (isAuthSuccess)
            {
                var userName = (await fireBaseAuth.GetProfileInfo()).User.DisplayName;
                var userData = (await realTimeDB.Client.Child(Strings.TableUsers + "/" + userName).OnceAsync<UserProfile>()).Select(element=> new UserProfile
                {
                    DisplayName = element.Object.DisplayName,
                    IsAdmin = element.Object.IsAdmin
                }).ToList();
                UserDialogs.Instance.HideLoading();
                Application.Current.MainPage = pages[userData[0].IsAdmin && IsAdmin];
                return;
            }
            UserDialogs.Instance.HideLoading();
            await DisplayAlert("ошибка", "пользователь с введенными данными не найден", "ок");
        }

        private async void SignUpTapped(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new SignUpPage(serviceManager));
        }

        private void Toggle_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(GreetingsText));
        }
    }
}