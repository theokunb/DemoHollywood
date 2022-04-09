using DemoHollywood.ViewModels.CustomControls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DemoHollywood.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToogleTwoImages : ContentView
    {
        public ToogleTwoImages()
        {
            InitializeComponent();

            (BindingContext as ToggleTwoImageViewModel).ResultChange = new Action<bool>(param =>
            {
                Result = param;
            });
        }

        #region LeftMainImageProperty
        public static readonly BindableProperty LeftMainImageProperty = BindableProperty.Create(nameof(LeftMainImage), typeof(ImageSource), typeof(ToogleTwoImages), null, propertyChanged: LeftMainImagePropertyChanged);
        
        private static void LeftMainImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable.BindingContext as ToggleTwoImageViewModel).LeftMainImage = newValue as ImageSource;
        }

        public ImageSource LeftMainImage
        {
            get { return GetValue(LeftMainImageProperty) as ImageSource; }
            set { SetValue(LeftMainImageProperty, value); }
        }
        #endregion

        #region LeftTextProperty
        public static readonly BindableProperty LeftTextProperty = BindableProperty.Create(nameof(LeftText),typeof(string),typeof(ToogleTwoImages),string.Empty,propertyChanged: LeftTextPropertyChanged);
        private static void LeftTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable.BindingContext as ToggleTwoImageViewModel).LeftText = newValue.ToString();
        }
        public string LeftText
        {
            get { return GetValue(LeftTextProperty).ToString(); }
            set { SetValue(LeftTextProperty, value); }
        }

        #endregion

        #region CheckImageProperty
        public static readonly BindableProperty CheckImageProperty = BindableProperty.Create(nameof(CheckImage), typeof(ImageSource), typeof(ToogleTwoImages), null, propertyChanged: CheckImagePropertyChanged);
        private static void CheckImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable.BindingContext as ToggleTwoImageViewModel).CheckImage = newValue as ImageSource;
        }
        public ImageSource CheckImage
        {
            get { return GetValue(CheckImageProperty) as ImageSource; }
            set { SetValue(CheckImageProperty, value); }
        }
        #endregion

        #region RightMainImageProperty
        public static readonly BindableProperty RightMainImageProperty = BindableProperty.Create(nameof(RightMainImage), typeof(ImageSource), typeof(ToogleTwoImages), null, propertyChanged: RightMainImagePropertyChanged);
        private static void RightMainImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable.BindingContext as ToggleTwoImageViewModel).RightMainImage = newValue as ImageSource;
        }
        public ImageSource RightMainImage
        {
            get { return GetValue(RightMainImageProperty) as ImageSource; }
            set { SetValue(RightMainImageProperty, value); }
        }
        #endregion

        #region RightTextProperty
        public static readonly BindableProperty RightTextProperty = BindableProperty.Create(nameof(RightText), typeof(string), typeof(ToogleTwoImages), string.Empty, propertyChanged: RightTextPropertyChanged);
        private static void RightTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable.BindingContext as ToggleTwoImageViewModel).RightText = newValue.ToString();
        }
        public string RightText
        {
            get { return GetValue(LeftTextProperty).ToString(); }
            set { SetValue(LeftTextProperty, value); }
        }

        #endregion

        #region ResultProperty
        public static readonly BindableProperty ResultProperty = BindableProperty.Create(nameof(Result), typeof(bool), typeof(ToogleTwoImages), false, BindingMode.TwoWay, propertyChanged: ResultPropertyChanged);

        private static void ResultPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable.BindingContext as ToggleTwoImageViewModel).Result = (bool)newValue;
        }

        public bool Result
        {
            get { return (bool)GetValue(ResultProperty); }
            set { SetValue(ResultProperty, value); }
        }

        #endregion
    }
}