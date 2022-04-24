using DemoHollywood.Helpers;
using DemoHollywood.Models;
using DemoHollywood.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoHollywood.ViewModels
{
    public class DocumentsViewModel : BaseViewModel
    {
        public DocumentsViewModel(ServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
            isLoad = false;
            IsRefreshing = false;
            Documents = new ObservableCollection<Document>();

            CommandAppearing = new Command((param) => OnAppearing(param));
            CommandBack = new Command(param => ButtonBack(param));
            CommandRefreshView = new Command(param => RefreshView(param));
        }


        private readonly ServiceManager serviceManager;
        private bool isLoad;
        private bool isRefreshing;

        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Document> Documents { get; set; }
        
        public ICommand CommandAppearing { get; }
        public ICommand CommandBack { get; }
        public ICommand CommandRefreshView { get; }


        private async Task UpdateView()
        {
            var documents = await serviceManager.RealTimeDB.GetDocuments(Strings.Documents);
            Documents.Clear();
            foreach (var element in documents)
                Documents.Add(element);
        }
        private void OnAppearing(object param)
        {
            if (isLoad)
                return;
            IsRefreshing = true;
            isLoad = true;
        }

        private async void RefreshView(object param)
        {
            await UpdateView();

            IsRefreshing = false;
        }

        private async void ButtonBack(object param)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
