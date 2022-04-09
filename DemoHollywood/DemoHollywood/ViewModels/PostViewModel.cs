using DemoHollywood.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoHollywood.ViewModels
{
    public class PostViewModel : BaseViewModel
    {
        public PostViewModel(NewsPost post)
        {
            Text = post.Text;
            Date = post.Date;
            Images = new ObservableCollection<ImageSource>();

            foreach(var image in post.Images)
            {
                Images.Add(image);
            }
            CommandBack = new Command((param) => BackTapped(param));
        }

        private string date;
        private string text;


        public ObservableCollection<ImageSource> Images { get; set; }
        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }
        public string Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }
        public ICommand CommandBack { get; }


        private async void BackTapped(object param)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
