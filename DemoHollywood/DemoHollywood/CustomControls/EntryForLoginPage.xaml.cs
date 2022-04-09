using DemoHollywood.ViewModels.CustomControls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoHollywood.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryForLoginPage : ContentView
    {
        public EntryForLoginPage()
        {
            InitializeComponent();
            (BindingContext as EntryLoginPageViewModel).TextChanged = new Action<string>((param) =>
             {
                 Text = param;
             });
        }

        #region ImageProperty
        public static readonly BindableProperty ImageProperty = BindableProperty.Create(nameof(Image),typeof(ImageSource),typeof(EntryForLoginPage),null,propertyChanged:ImagePropertyChanged);

        private static void ImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable.BindingContext as EntryLoginPageViewModel).Image = newValue as ImageSource;
        }

        public ImageSource Image
        {
            get { return GetValue(ImageProperty) as ImageSource; }
            set { SetValue(ImageProperty, value); }
        }
        #endregion

        #region TextProperty
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryForLoginPage), string.Empty, propertyChanged: TextPropertyyChanged);

        private static void TextPropertyyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable.BindingContext as EntryLoginPageViewModel).Text = newValue.ToString();
        }

        public string Text
        {
            get { return GetValue(TextProperty).ToString(); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region PlaceholderProperty
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder),typeof(string),typeof(EntryForLoginPage),string.Empty,propertyChanged:PlaceholderPropertyChanged);

        private static void PlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable.BindingContext as EntryLoginPageViewModel).Placeholder = newValue.ToString();
        }

        public string Placeholder
        {
            get { return GetValue(PlaceholderProperty).ToString(); }
            set { SetValue(PlaceholderProperty, value); }
        }
        #endregion

        #region IsPasswordProperty
        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword),typeof(bool),typeof(EntryForLoginPage),false,propertyChanged:IsPasswordPropertyChanged);

        private static void IsPasswordPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable.BindingContext as EntryLoginPageViewModel).IsPassword = (bool)newValue;
        }

        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }
        #endregion

    }
}