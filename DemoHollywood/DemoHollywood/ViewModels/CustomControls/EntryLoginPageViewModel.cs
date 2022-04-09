using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoHollywood.ViewModels.CustomControls
{
    public class EntryLoginPageViewModel : BaseViewModel
    {
        public EntryLoginPageViewModel()
        {
            CommandTextChanged = new Command((param) =>
            {
                TextChanged(Text);
            });
            CommandFocused = new Command((param) =>
            {
                if (param.Equals("1"))
                    IsFocused = true;
                else
                    IsFocused = false;
            });
        }

        private ImageSource image;
        private string text;
        private string placeholder;
        private bool isPassword;
        private bool isFocused = false;

        public Action<string> TextChanged { get; set; }
        public ICommand CommandFocused { get; }
        public ICommand CommandTextChanged { get; }

        public bool IsFocused
        {
            get => isFocused;
            set
            {
                isFocused = value;
                OnPropertyChanged();
            }
        }
        public bool IsPassword
        {
            get => isPassword;
            set
            {
                isPassword = value;
                OnPropertyChanged();
            }
        }
        public string Placeholder
        {
            get => placeholder;
            set
            {
                placeholder = value;
                OnPropertyChanged();
            }
        }
        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }
        public ImageSource Image
        {
            get => image;
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }


    }
}
