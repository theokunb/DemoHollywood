using Acr.UserDialogs;
using DemoHollywood.Helpers;
using DemoHollywood.Models;
using DemoHollywood.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoHollywood.ViewModels
{
    public class ServicesViewModel : BaseViewModel
    {
        public ServicesViewModel(RealTimeDB realTimeDB, ReueqstAppointments request)
        {
            this.realTimeDB = realTimeDB;
            this.request = request;
            CommandAppearing = new Command((param) => OnAppearing(param));
            CommandBack = new Command((param) => BackButtonTapped(param));
            CommandButtonReserve = new Command((param) => ReserveButtonTapped(param));
        }



        private readonly RealTimeDB realTimeDB;
        private readonly ReueqstAppointments request;
        private Service currentService;
        

        public ObservableCollection<Service> AvailableServices { get; set; } = new ObservableCollection<Service>();
        public ICommand CommandAppearing { get; }
        public ICommand CommandBack { get; }
        public ICommand CommandButtonReserve { get; }
        public Service CurrentService
        {
            get => currentService;
            set
            {
                currentService = value;
                OnPropertyChanged();
            }
        }


        private void Subscribe()
        {
            AvailableServices.Clear();
            var collection = realTimeDB.Client.Child(Strings.TableServices).AsObservable<Service>().Subscribe((dbEvent) =>
            {
                if (dbEvent.Object != null)
                {
                    if (AvailableServices.ToList().Exists(service => service.Title == dbEvent.Object.Title))
                    {
                        var repeating = AvailableServices.ToList().Find(appoint => appoint.Title == dbEvent.Object.Title);
                        AvailableServices[AvailableServices.IndexOf(repeating)] = dbEvent.Object;
                    }
                    else
                        AvailableServices.Add(dbEvent.Object);
                }
            });
        }
        private void OnAppearing(object param)
        {
            Subscribe();
        }

        private async void BackButtonTapped(object param)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public async void ReserveButtonTapped(object param)
        {
            UserDialogs.Instance.ShowLoading("Записываемся на приём...");
            var resCollection = await realTimeDB.CanAddAppointment(request.TargetTable, request.Key, currentService.Duration);

            if (!resCollection.Key)
            {
                UserDialogs.Instance.HideLoading();
                await Application.Current.MainPage.DisplayAlert("Внимание", "Выбранная услуга не может быть оказана в выбранное время", "ок");
                return;
            }

            foreach(var appoint in resCollection.Value)
            {
                var newRequest = new ReueqstAppointments()
                {
                    Key = appoint.Key,
                    Appointment = appoint.Value,
                    Author = request.Author,
                    TargetTable = request.TargetTable
                };
                newRequest.Appointment.Reserve(newRequest.Author, CurrentService.Title);
                newRequest.Appointment.ReferenceKey = request.Key;
                realTimeDB.Post(newRequest);
            }
            UserDialogs.Instance.HideLoading();
            await Application.Current.MainPage.DisplayAlert("Успешно", "Заявка принята в обработку", "ок");
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
