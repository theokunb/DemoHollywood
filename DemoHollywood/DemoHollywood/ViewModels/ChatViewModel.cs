using DemoHollywood.Helpers;
using DemoHollywood.Models;
using DemoHollywood.Services;
using Firebase.Auth;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace DemoHollywood.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        public ChatViewModel(User user, RealTimeDB realTimeDB)
        {
            this.user = user;
            this.realTimeDB = realTimeDB;

            Messages = new ObservableCollection<Message>();
            CommandButtonSend = new Command((param) => ButtonSend(param));
            CommandAppearing = new Command((param) => Appearing(param));
            CommandBack = new Command(param => ButtonBack(param));
        }


        private User user;
        private RealTimeDB realTimeDB;
        private string text;
        


        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanSendMessage));
            }
        }
        public bool CanSendMessage => !string.IsNullOrEmpty(Text);

        public ICommand CommandBack { get; }
        public ICommand CommandButtonSend { get; }
        public ICommand CommandAppearing { get; }
        public ObservableCollection<Message> Messages { get; set; }



        private void ButtonSend(object param)
        {
            Message message = new Message
            {
                Author = user.DisplayName,
                Text = Text
            };
            realTimeDB.Post(message);
            Text = string.Empty;
        }

        private void Appearing(object param)
        {
            Subscribe();
        }

        private void Subscribe()
        {
            var collection = realTimeDB.Client.Child(Strings.TableChat).AsObservable<Message>().Subscribe((dbEvent) =>
            {
                if (dbEvent != null)
                {
                    Messages.Add(dbEvent.Object);
                }
            });
        }
        private async void ButtonBack(object param)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
