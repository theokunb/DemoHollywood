using DemoHollywood.Helpers;
using DemoHollywood.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoHollywood.ViewModels
{
    public class DocumentsViewModel : BaseViewModel
    {
        public DocumentsViewModel(Storage storage)
        {
            this.storage = storage;
            isLoad = false;
            CommandAppearing = new Command((param) => OnAppearing(param));
        }


        private readonly Storage storage;
        private bool isLoad;

        public ICommand CommandAppearing { get; }

        private async void OnAppearing(object param)
        {
            if (isLoad)
                return;

            isLoad = true;
        }
    }
}
