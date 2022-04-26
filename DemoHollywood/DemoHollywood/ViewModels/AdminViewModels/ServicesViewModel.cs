using DemoHollywood.Helpers;
using DemoHollywood.Models;
using DemoHollywood.Services;
using DemoHollywood.Views.AdminViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoHollywood.ViewModels.AdminViewModels
{
    public class ServicesViewModel : BaseViewModel
    {
        public ServicesViewModel() { }
        public ServicesViewModel(ServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
            Services = new ObservableCollection<Service>();
            isViewRefreshing = false;


            CommandAppearing = new Command(param => OnAppearing(param));
            CommandBack = new Command(param => ButtonBack(param));
            CommandAddService = new Command(param => ButtonAddService(param));
            CommandRefreshView = new Command(param => OnRefreshView(param));
            CommandEdit = new Command(param => ButtonEdit(param));
        }


        private readonly ServiceManager serviceManager;
        private bool isViewRefreshing;



        public bool IsViewRefreshing
        {
            get { return isViewRefreshing; }
            set 
            {
                isViewRefreshing = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Service> Services { get; set; }
        public ICommand CommandBack { get; }
        public ICommand CommandAddService { get; }
        public ICommand CommandAppearing { get; }
        public ICommand CommandRefreshView { get; }
        public ICommand CommandEdit { get; }


        private async void ButtonBack(object param)
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }
        private async void ButtonAddService(object param)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new AddServicePage(serviceManager));
        }
        private void OnAppearing(object param)
        {
            IsViewRefreshing = true;
        }
        private async void OnRefreshView(object param)
        {
            Services.Clear();
            var services = await serviceManager.RealTimeDB.GetServices(Strings.TableServices);
            foreach(var element in services)
            {
                Services.Add(element);
            }
            IsViewRefreshing = false;
        }
        private void ButtonEdit(object param)
        {

        }
    }
}
