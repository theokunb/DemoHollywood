using Acr.UserDialogs;
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
            Services = new ObservableCollection<KeyValuePair<string, Service>>();
            isViewRefreshing = false;
            isLoaded = false;

            CommandAppearing = new Command(param => OnAppearing(param));
            CommandBack = new Command(param => ButtonBack(param));
            CommandAddService = new Command(param => ButtonAddService(param));
            CommandRefreshView = new Command(param => OnRefreshView(param));
            CommandSave = new Command(param => ButtonSave(param));
            CommandEditImage = new Command(param => ButtonEditImage(param));
            CommandRemoveService = new Command(param => RemoveService(param));
        }

        

        private readonly ServiceManager serviceManager;
        private bool isViewRefreshing;
        private bool isLoaded;
        private KeyValuePair<string, Service> currentService;

        public bool IsViewRefreshing
        {
            get { return isViewRefreshing; }
            set 
            {
                isViewRefreshing = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<KeyValuePair<string, Service>> Services { get; set; }
        public KeyValuePair<string, Service> CurrentService
        {
            get => currentService;
            set
            {
                currentService = value;
                OnPropertyChanged();
            }
        }
        public ICommand CommandBack { get; }
        public ICommand CommandAddService { get; }
        public ICommand CommandAppearing { get; }
        public ICommand CommandRefreshView { get; }
        public ICommand CommandSave { get; }
        public ICommand CommandEditImage { get; }
        public ICommand CommandRemoveService { get; }


        private async void ButtonBack(object param)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        private async void ButtonAddService(object param)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddServicePage(serviceManager));
        }
        private void OnAppearing(object param)
        {
            if (!isLoaded)
            {
                IsViewRefreshing = true;
                isLoaded = true;
            }
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
        private async void ButtonSave(object param)
        {
            if (isBusy)
                return;
            isBusy = true;
            if(param is KeyValuePair<string,Service> service)
            {
                if (!service.Value.IsValid())
                {
                    isBusy = false;
                    return;
                }
                await serviceManager.RealTimeDB.PatchDocument(Strings.TableServices + "/" + service.Key, service.Value);
                IsViewRefreshing = true;
            }
            isBusy = false;
        }
        private async void ButtonEditImage(object param)
        {
            if (isBusy)
                return;
            isBusy = true;

            if (param is KeyValuePair<string, Service> service)
            {
                var newImage = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();
                if (newImage == null)
                    return;
                var uploadTask = await serviceManager.Storage.PutDocument(Strings.TableServices, newImage);
                string link = await uploadTask;
                service.Value.ImageName = newImage.FileName;
                service.Value.ImagePath = link;

                await serviceManager.RealTimeDB.PatchDocument(Strings.TableServices + "/" + service.Key, service.Value);
                IsViewRefreshing = true;
            }
            isBusy = false;
        }
        private async void RemoveService(object param)
        {
            if (CurrentService.Value == null)
                return;
            if (isBusy)
                return;
            isBusy = true;

            var dialogResult = await Application.Current.MainPage.DisplayAlert("Внимание", "Вы действительно хотите удалить данную услугу,", "Да", "Нет");
            if (!dialogResult)
            {
                isBusy = false;
                return;
            }
            await serviceManager.RealTimeDB.RemoveService(CurrentService.Key);
            IsViewRefreshing = true;
            isBusy = false;
        }
    }
}
