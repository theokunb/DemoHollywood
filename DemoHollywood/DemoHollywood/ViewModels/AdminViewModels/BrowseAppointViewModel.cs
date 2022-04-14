using DemoHollywood.Helpers;
using DemoHollywood.Models;
using DemoHollywood.Services;
using DemoHollywood.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DemoHollywood.ViewModels.AdminViewModels
{
    public class BrowseAppointViewModel : BaseViewModel
    {
        public BrowseAppointViewModel(ServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
            Appointments = new ObservableCollection<AppointmentBig>();

            CommandAppearing = new Command((param) => OnAppearing(param));
            CommandButtonLogout = new Command((param) => ButtonLogOutPerform(param));
            CommandDateSelected = new Command((param) => OnDateSelected(param));
        }

        private DateTime selectedDate;
        private readonly ServiceManager serviceManager;


        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                OnPropertyChanged();
            }
        }
        public ICommand CommandButtonLogout { get; }
        public ICommand CommandAppearing { get; }
        public ICommand CommandDateSelected { get; }
        public ObservableCollection<AppointmentBig> Appointments { get; set; }


        private void ButtonLogOutPerform(object param)
        {
            Preferences.Remove(Strings.AuthToken);
            Preferences.Remove(Strings.PermissionToken);
            Application.Current.MainPage = new LoginPage(serviceManager);
        }
        private void OnAppearing(object param)
        {
            SelectedDate = DateTime.Now;
        }
        private async void OnDateSelected(object param)
        {
            Appointments.Clear();
            var collection = await serviceManager.RealTimeDB.GetAppointents(SelectedDate.ToShortDateString().Replace('.', ':'));
            foreach(var element in collection)
            {
                if (!string.IsNullOrEmpty(element.Value.AppointedUser))
                {
                    var appointedUser = await serviceManager.RealTimeDB.GetUserData(Strings.TableUsers + "/" + element.Value.AppointedUser);
                    Appointments.Add(new AppointmentBig(element.Value, appointedUser.Value));
                }
            }
        }
    }
}
