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
        public ServicesViewModel() { }
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



        private async void OnAppearing(object param)
        {
            var services = await realTimeDB.GetServices(Strings.TableServices);

            foreach(var service in services)
            {
                AvailableServices.Add(service.Value);
            }
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
