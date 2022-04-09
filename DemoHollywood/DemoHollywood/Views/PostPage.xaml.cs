using DemoHollywood.Models;
using DemoHollywood.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoHollywood.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostPage : ContentPage
    {
        public PostPage(NewsPost post)
        {
            InitializeComponent();
            BindingContext = new PostViewModel(post);
        }
    }
}