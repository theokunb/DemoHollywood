using DemoHollywood.ViewModels.CustomControls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoHollywood.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomEditor : ContentView
    {
        public CustomEditor()
        {
            InitializeComponent();
            (BindingContext as CustomEditorViewModel).TextChanged = new Action<string>((param) =>
            {
                Text = param;
            });
        }

        #region TextProperty
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomEditor), string.Empty, propertyChanged: TextPropertyyChanged);

        private static void TextPropertyyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable.BindingContext as CustomEditorViewModel).Text = newValue.ToString();
        }

        public string Text
        {
            get { return GetValue(TextProperty).ToString(); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region PlaceholderProperty
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CustomEditor), string.Empty, propertyChanged: PlaceholderPropertyChanged);

        private static void PlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable.BindingContext as CustomEditorViewModel).Placeholder = newValue.ToString();
        }

        public string Placeholder
        {
            get { return GetValue(PlaceholderProperty).ToString(); }
            set { SetValue(PlaceholderProperty, value); }
        }
        #endregion
    }
}