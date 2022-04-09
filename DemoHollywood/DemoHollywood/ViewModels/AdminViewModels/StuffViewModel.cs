using DemoHollywood.Helpers;
using DemoHollywood.Models;
using DemoHollywood.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;

namespace DemoHollywood.ViewModels.AdminViewModels
{
    public class StuffViewModel : BaseViewModel
    {
        public StuffViewModel(FireBaseAuth fireBaseAuth, RealTimeDB realTimeDB)
        {
            this.fireBaseAuth = fireBaseAuth;
            this.realTimeDB = realTimeDB;
            Users = new ObservableCollection<UserProfile>();

            CommandOnAppearing = new Command((param) => OnAppearing(param));
            CommandOnSearchTextChanged = new Command((param) => OnSearchTextChanged(param));
            CommandToggled = new Command((param) => OnToggled(param));

            timer = new System.Timers.Timer();
            timer.Elapsed += (sender, e) =>
            {
                GetUsers();
            };
            timer.Interval = 1000;
        }


        private string userName;
        private readonly FireBaseAuth fireBaseAuth;
        private readonly RealTimeDB realTimeDB;
        private string searchText;
        private Timer timer;

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged();
            }
        }
        public ICommand CommandOnAppearing { get; }
        public ICommand CommandOnSearchTextChanged { get; }
        public ICommand CommandToggled { get; }
        public ObservableCollection<UserProfile> Users { get; set; }


        private async void OnAppearing(object param)
        {
            UserName = (await fireBaseAuth.GetProfileInfo()).User.DisplayName;
        }

        private async void GetUsers()
        {
            var users = await realTimeDB.GetUsers(Strings.TableUsers, SearchText);

            for (int i = Users.Count - 1; i >= 0; i--)
            {
                if (!Users[i].DisplayName.Contains(SearchText))
                    Users.RemoveAt(i);
            }

            foreach (var user in users)
            {
                var userData = await realTimeDB.GetUserData(Strings.TableUsers + "/" + user);
                if (!Users.ToList().Exists(element => element.DisplayName == userData.Value.DisplayName))
                    Users.Add(userData.Value);
            }
        }
        private void OnSearchTextChanged(object param)
        {
            if(string.IsNullOrEmpty(SearchText))
            {
                testTask?.Wait();
                Users.Clear();
                return;
            }
            if (!timer.Enabled)
                timer.Start();
            else
            {
                timer.Stop();
                timer.Start();
            }
        }

        private Task testTask;
        private void OnToggled(object param)
        {
            testTask = Task.Factory.StartNew(async () =>
           {
               var user = param as UserProfile;
               var userData = await realTimeDB.GetUserData(Strings.TableUsers + "/" + user.DisplayName);
               if (user != userData.Value)
               {
                   var request = new RequestChangeUserProfile()
                   {
                       Author = UserName,
                       Key = userData.Key,
                       UserProfile = user,
                       TargetTable = Strings.TableUsers + "/" + user.DisplayName
                   };
                   realTimeDB.Post(request);
               }
           });
        }

    }
}
