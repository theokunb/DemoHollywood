using DemoHollywood.Services;
using DemoHollywood.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoHollywood.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentsPage : ContentPage
    {
        public DocumentsPage(ServiceManager serviceManager)
        {
            InitializeComponent();
            BindingContext = new DocumentsViewModel(serviceManager);
        }
    }
}