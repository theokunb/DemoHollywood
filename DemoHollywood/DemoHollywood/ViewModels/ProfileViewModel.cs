using Acr.UserDialogs;
using DemoHollywood.Helpers;
using DemoHollywood.Models;
using DemoHollywood.Services;
using DemoHollywood.Views;
using Firebase.Auth;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DemoHollywood.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public ProfileViewModel(User currentUser, ServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
            this.currentUser = currentUser;

            CommandButtonQuit = new Command((param) => ButtonQuitTapped(param));
            CommandOnAppearing = new Command((param) => OnAppearing(param));
            CommandAppointSelected = new Command((param) => OnAppointSelected(param));
            CommandPullRefresh = new Command((param) => ListViewPulRefresh(param));

            appointParser = new AppointParser();
        }

        private AppointParser appointParser;
        private readonly ServiceManager serviceManager;
        private readonly User currentUser;
        private bool isListViewRefreshing;

        public string UserName => currentUser.DisplayName;
        public ICommand CommandButtonQuit { get; }
        public ICommand CommandOnAppearing { get; }
        public ICommand CommandAppointSelected { get; }
        public ICommand CommandPullRefresh { get; }
        public ObservableCollection<AppointGroup> Appointments { get; set; } = new ObservableCollection<AppointGroup>();
        
        public bool IsListViewRefreshing
        {
            get => isListViewRefreshing;
            set
            {
                isListViewRefreshing = value;
                OnPropertyChanged();
            }
        }


        private async void ButtonQuitTapped(object param)
        {
            var dialogResult = await Application.Current.MainPage.DisplayAlert("Внимание", "Вы действительно хотите выйти из системы?", "Да", "Нет");

            if (!dialogResult)
                return;

            Preferences.Remove(Strings.AuthToken);
            Preferences.Remove(Strings.PermissionToken);
            Application.Current.MainPage = new LoginPage(serviceManager);
        }

        private async void OnAppearing(object param)
        {
            if(Appointments.Count == 0)
            {
                var res = await serviceManager.RealTimeDB.GetAppointents(Strings.TableAppointments + "/" + UserName);
                var displayCollection = appointParser.Parse(res);
                foreach (var element in displayCollection)
                    Appointments.Add(element);
            }
        }

        private async void OnAppointSelected(object param)
        {
            var eventArgs = param as ItemTappedEventArgs;
            var selectedItem = eventArgs.Item as AppointGroup;

            var result = await Application.Current.MainPage.DisplayAlert("внимание", "отменить выбранную запись?", "да", "нет");
            if (!result)
                return;

            UserDialogs.Instance.ShowLoading("Отменяем");
            foreach (var element in selectedItem.Appoints)
            {
                var request = new RequestRemoveAppointment()
                {
                    Author = UserName,
                    Key = element.Key,
                    TargetTable = element.Value.DisplayDate.Replace('.', ':'),
                    AppointKey = element.Value.AppointKey
                };
                serviceManager.RealTimeDB.Post(request);
            }
            UserDialogs.Instance.HideLoading();
        }
        private async void ListViewPulRefresh(object param)
        {
            Appointments.Clear();
            var res = await serviceManager.RealTimeDB.GetAppointents(Strings.TableAppointments + "/" + UserName);
            var displayCollection = appointParser.Parse(res);
            foreach (var element in displayCollection)
                Appointments.Add(element);
            IsListViewRefreshing = false;
        }
    }
}
