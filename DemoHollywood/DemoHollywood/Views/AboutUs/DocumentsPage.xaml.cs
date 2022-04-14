using DemoHollywood.Services;
using DemoHollywood.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoHollywood.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DocumentsPage : ContentPage
    {
        public DocumentsPage(Storage storage)
        {
            InitializeComponent();
            BindingContext = new DocumentsViewModel(storage);
        }
    }
}