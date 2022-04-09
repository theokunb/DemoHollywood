using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoHollywood.ViewModels.CustomControls
{
    public class ToggleTwoImageViewModel : BaseViewModel
    {
        public ToggleTwoImageViewModel()
        {
            CommandTapped = new Command((param) => OnTapped(param));
        }


        private ImageSource leftMainImage;
        private string leftText;
        private ImageSource checkImage;
        private ImageSource rightMainImage;
        private string rightText;
        private bool result;

        public Action<bool> ResultChange { get; set; }
        public ICommand CommandTapped { get; set; }
        public ImageSource LeftMainImage
        {
            get => leftMainImage;
            set
            {
                leftMainImage = value;
                OnPropertyChanged();
            }
        }
        public string LeftText
        {
            get => leftText;
            set
            {
                leftText = value;
                OnPropertyChanged();
            }
        }
        public ImageSource CheckImage
        {
            get => checkImage;
            set
            {
                checkImage = value;
                OnPropertyChanged();
            }
        }
        public ImageSource RightMainImage
        {
            get => rightMainImage;
            set
            {
                rightMainImage = value;
                OnPropertyChanged();
            }
        }
        public string RightText
        {
            get => rightText;
            set
            {
                rightText = value;
                OnPropertyChanged();
            }
        }
        public bool Result
        {
            get => result;
            set
            {
                result = value;
                OnPropertyChanged();
            }
        }

        private void OnTapped(object param)
        {
            if (int.TryParse(param.ToString(),out int num))
            {
                if(num == 0)
                {
                    Result = true;
                }
                else
                {
                    Result = false;
                }
                ResultChange(Result);
            }
        }
    }
}
