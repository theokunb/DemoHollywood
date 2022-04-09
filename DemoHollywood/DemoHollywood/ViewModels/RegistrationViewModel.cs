using DemoHollywood.Helpers;
using DemoHollywood.Models;
using DemoHollywood.Services;
using DemoHollywood.Views;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoHollywood.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        public RegistrationViewModel(User currentUser, RealTimeDB realTimeDB)
        {
            CommandAppearing = new Command((param) => OnAppearing(param));
            CommandDateSelected = new Command((param) => OnDateSelected(param));
            CommandAppointSelected = new Command((param) => AppointmentSelected(param));
            user = currentUser;
            this.realTimeDB = realTimeDB;
            pageLoaded = false;
        }


        private readonly User user;
        private readonly RealTimeDB realTimeDB;
        private string userName;
        private DateTime selectedDate;
        private bool pageLoaded;
        private string tableName => SelectedDate.ToShortDateString().Replace('.', ':');


        public ObservableCollection<KeyValuePair<string, Appointment>> Appointments { get; set; } = new ObservableCollection<KeyValuePair<string, Appointment>>();
        public ICommand CommandAppointSelected { get; }
        public ICommand CommandAppearing { get; }
        public ICommand CommandDateSelected { get; }
        public ICommand CommandAddApointments { get; }


        public string DisplayDescription => Strings.DescriptionRegistrationPage;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }
        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                OnPropertyChanged();
            }
        }



        private void OnAppearing(object param)
        {
            if (!pageLoaded)
            {
                UserName = user.DisplayName;
                SelectedDate = DateTime.Now;
                pageLoaded = true;
            }
        }
        private void OnDateSelected(object param)
        {
            Subscribe(tableName);
        }

        private async void AppointmentSelected(object param)
        {
            var eventArgs = param as ItemTappedEventArgs;
            var selectedItem = (KeyValuePair<string, Appointment>)eventArgs.Item;

            var dialogResult = await Application.Current.MainPage.DisplayAlert("Запись на приём", $"Выбрать дату {tableName}, время {selectedItem.Value.TimeOfAppointment}", "да", "нет");
            if (dialogResult)
            {
                var request = new ReueqstAppointments()
                {
                    Key = selectedItem.Key,
                    Appointment = selectedItem.Value,
                    Author = UserName,
                    TargetTable = tableName,
                };
                await Application.Current.MainPage.Navigation.PushModalAsync(new ServicesPage(realTimeDB, request));
            }
        }

        private void Subscribe(string tableName)
        {
            Appointments.Clear();
            var collection = realTimeDB.Client.Child(tableName).AsObservable<Appointment>().Subscribe((dbEvent) =>
            {
                if (dbEvent.Object != null)
                {
                    if (Appointments.ToList().Exists(appoint => appoint.Value.TimeOfAppointment == dbEvent.Object.TimeOfAppointment))
                    {
                        var repeating = Appointments.ToList().Find(appoint => appoint.Value.TimeOfAppointment == dbEvent.Object.TimeOfAppointment);
                        Appointments[Appointments.IndexOf(repeating)] = new KeyValuePair<string, Appointment>(dbEvent.Key, dbEvent.Object);
                    }
                    else 
                        Appointments.Add(new KeyValuePair<string, Appointment>(dbEvent.Key, dbEvent.Object));
                }
            });
        }
    }
}
