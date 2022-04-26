using DemoHollywood.Services;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using System;
using DemoHollywood.Helpers;
using DemoHollywood.Models;

namespace DemoHollywood.ViewModels.AdminViewModels
{
    public class AddServiceViewModel : BaseViewModel
    {
        public AddServiceViewModel() { }
        public AddServiceViewModel(ServiceManager serviceManager)
        {
            isBusy = false;
            this.serviceManager = serviceManager;

            CommandBack = new Command(param => ButtonBack(param));
            CommandSelectFile = new Command(param => ButtonSelectFile(param));
            CommandAccept = new Command(param => ButtonAccept(param));
        }


        private readonly ServiceManager serviceManager;
        private FileResult selectedFile;
        private string title;
        private string description;
        private string duration;
        private string price;
        private string filePath;

        public string FilePath
        {
            get => filePath;
            set
            {
                filePath = value;
                OnPropertyChanged();
            }
        }
        public string Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }
        public string Duration
        {
            get => duration;
            set
            {
                duration = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }
        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }
        public ICommand CommandSelectFile { get; }
        public ICommand CommandBack { get; }
        public ICommand CommandAccept { get; }


        private async void ButtonBack(object param)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        private async void ButtonSelectFile(object param)
        {
            if (isBusy)
                return;
            isBusy = true;
            selectedFile = await MediaPicker.PickPhotoAsync();
            FilePath = selectedFile.FullPath;

            isBusy = false;
        }
        private async void ButtonAccept(object param)
        {
            if (isBusy)
                return;
            if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(Duration) || string.IsNullOrEmpty(Price) || selectedFile == null)
                return;
            isBusy = true;

            var storageTask = await serviceManager.Storage.PutDocument(Strings.TableServices, selectedFile);
            var link = await storageTask;

            Service newService = new Service()
            {
                Title = Title,
                Description = Description,
                Duration = Convert.ToInt16(Duration),
                Price = Convert.ToInt32(Price),
                ImageName = selectedFile.FileName,
                ImagePath = link
            };
            await serviceManager.RealTimeDB.PostDocument(Strings.TableServices, newService);

            await Application.Current.MainPage.DisplayAlert("Готово", "Услуга добавлена", "ок");

            isBusy = false;
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
