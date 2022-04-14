using DemoHollywood.Helpers;
using DemoHollywood.Models;
using DemoHollywood.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoHollywood.ViewModels.AdminViewModels
{
    public class DocumentsViewmodel : BaseViewModel
    {
        public DocumentsViewmodel() { }
        public DocumentsViewmodel(ServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
            isUploading = false;
            uploadingProgress = 0;

            Documents = new ObservableCollection<Document>();

            CommandBack = new Command(param => ButtonBack(param));
            CommandOnAppearing = new Command(param => Appearing(param));
            CommandAddDocument = new Command(param => AddDocument(param));
            CommandRefreshView = new Command(param => OnRefreshView(param));
            CommandRemoveItem = new Command(param => RemoveDocument(param));
        }


        private readonly ServiceManager serviceManager;
        private double uploadingProgress;
        private bool isUploading;
        private bool isRefreshingView;

        public bool IsUploading
        {
            get => isUploading;
            set
            {
                isUploading = value;
                OnPropertyChanged();
            }
        }
        public double UploadingProgress
        {
            get => uploadingProgress;
            set
            {
                uploadingProgress = value;
                OnPropertyChanged();
            }
        }
        public bool IsRefreshingView
        {
            get => isRefreshingView;
            set
            {
                isRefreshingView = value;
                OnPropertyChanged();
            }
        }

        public ICommand CommandBack { get; }
        public ICommand CommandAddDocument { get; }
        public ICommand CommandOnAppearing { get; }
        public ICommand CommandRefreshView { get; }
        public ICommand CommandRemoveItem { get; }
        public ObservableCollection<Document> Documents { get; set; }


        private async void AddDocument(object param)
        {
            var photo = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();

            if (photo == null)
                return;

            var uploadTask = await serviceManager.Storage.PutDocument(Strings.Documents, photo);
            IsUploading = true;

            uploadTask.Progress.ProgressChanged += (sender, args) =>
            {
                UploadingProgress = args.Percentage;
            };
            string link = await uploadTask;
            var document = new Document(link, photo.FileName);

            await serviceManager.RealTimeDB.PostDocument(Strings.Documents, document);

            await UpdateCollection();

            UploadingProgress = 0;
            IsUploading = false;
        }

        private async void RemoveDocument(object param)
        {
            var dialogResult = await Application.Current.MainPage.DisplayAlert("Внимание", "Вы действительно хотите удалить данный документ?", "Да", "Нет");
            if (!dialogResult)
                return;

            var document = param as Document;
            await serviceManager.Storage.RemoveDocument(Strings.Documents, document.FileName);
            await serviceManager.RealTimeDB.RemoveDocument(Strings.Documents, document.FileName);

            await UpdateCollection();
        }

        private async void Appearing(object param)
        {
            await UpdateCollection();
        }

        private async Task UpdateCollection()
        {
            var collection = await serviceManager.RealTimeDB.GetDocument(Strings.Documents);
            for(int i = Documents.Count - 1; i >= 0; i--)
            {
                if (collection.Exists(element => element.Link == Documents[i].Link))
                    continue;
                else
                    Documents.RemoveAt(i);
            }
            foreach(var element in collection)
            {
                if (Documents.ToList().Exists(document => document.Link == element.Link))
                    continue;
                else
                    Documents.Add(element);
            }
        }

        private async void OnRefreshView(object param)
        {
            await UpdateCollection();
            IsRefreshingView = false;
        }

        private async void ButtonBack(object param)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
