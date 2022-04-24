using DemoHollywood.Services;
using DemoHollywood.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoHollywood.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        public NewsPage(ServiceManager serviceManager)
        {
            InitializeComponent();
            BindingContext = new NewsViewModel(serviceManager);
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {

        }
    }
}