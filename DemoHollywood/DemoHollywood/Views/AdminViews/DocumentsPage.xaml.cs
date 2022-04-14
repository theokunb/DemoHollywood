using DemoHollywood.Services;
using DemoHollywood.ViewModels.AdminViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoHollywood.Views.AdminViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentsPage : ContentPage
    {
        public DocumentsPage(ServiceManager serviceManager)
        {
            InitializeComponent();
            BindingContext = new DocumentsViewmodel(serviceManager);
        }
    }
}