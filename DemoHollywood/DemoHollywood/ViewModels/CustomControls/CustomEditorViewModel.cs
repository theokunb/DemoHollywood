using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoHollywood.ViewModels.CustomControls
{
    public class CustomEditorViewModel : BaseViewModel
    {
        public CustomEditorViewModel()
        {
            isFocused = false;

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

        private string text;
        private string placeholder;
        private bool isFocused;



        public Action<string> TextChanged { get; set; }
        public ICommand CommandTextChanged { get; }
        public ICommand CommandFocused { get; }
        public bool IsFocused
        {
            get => isFocused;
            set
            {
                isFocused = value;
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
    }
}
