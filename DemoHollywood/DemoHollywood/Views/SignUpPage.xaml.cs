using Acr.UserDialogs;
using DemoHollywood.Helpers;
using DemoHollywood.Models;
using DemoHollywood.Services;
using Firebase.Database.Query;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoHollywood.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage(FireBaseAuth fireBaseAuth, RealTimeDB realTimeDB)
        {
            InitializeComponent();
            this.fireBaseAuth = fireBaseAuth;
            this.realTimeDB = realTimeDB;
            BindingContext = this;
        }

        private readonly FireBaseAuth fireBaseAuth;
        private readonly RealTimeDB realTimeDB;
        private string email;
        private string phone;
        private string userName;
        private string password;
        private bool agreementAccepted;


        public bool AgreementAccepted
        {
            get => agreementAccepted;
            set
            {
                agreementAccepted = value;
                OnPropertyChanged();
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
        public string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnPropertyChanged();
            }
        }
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
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

        private async void ButtonSignUpClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Внимание", "Вы заполнили не все данные", "ок");
                return;
            }
            UserDialogs.Instance.ShowLoading("Загрузка...");
            var signUpResult = await fireBaseAuth.CreateUser(Email, Password, UserName);
            if (signUpResult.Key)
            {
                var userProfile = new UserProfile()
                {
                    DisplayName = UserName,
                    IsAdmin = false,
                    PhoneNumber = Phone
                };
                await realTimeDB.Client.Child(Strings.TableUsers + "/" + UserName).PostAsync(userProfile);
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("успешно", "учетная запись создана", "ок");
                await Navigation.PopModalAsync();
            }
            else
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("ошибка", "эл. почта уже занята", "ок");
                Email = string.Empty;
            }
        }

        private async void BackImageTapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}