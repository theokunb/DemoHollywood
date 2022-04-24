using Acr.UserDialogs;
using DemoHollywood.Models;
using DemoHollywood.Models.Vk;
using DemoHollywood.Models.Vk.Post;
using DemoHollywood.Services;
using DemoHollywood.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DemoHollywood.ViewModels
{
    public class NewsViewModel : BaseViewModel
    {
        public NewsViewModel(ServiceManager serviceManager)
        {
            vkClient = serviceManager.VkClient;
            paramOffsetVal = "0";
            NewsPosts = new ObservableCollection<NewsPost>();
            IsViewRefreshing = false;


            CommandNewsTapped = new Command((param) => NewsPageSelected(param));
            CommandScroll = new Command((param) => OnScrolled(param));
            CommandAppearing = new Command((param) => OnAppearing(param));
            CommandViewRefresh = new Command(param => OnViewRefresh(param));
        }


        private VkClient vkClient;
        private string paramOffsetVal;
        private VkRequest vkRequest;
        private static KeyValuePair<string, string> paramDomain = new KeyValuePair<string, string>("domain", "hollywood_dental_ykt");
        private static KeyValuePair<string, string> paramCount = new KeyValuePair<string, string>("count", "10");
        private static KeyValuePair<string, string> paramFilter = new KeyValuePair<string, string>("filter", "owner");
        private const string paramOffsetKey = "offset";
        private const string methodTitle = "wall.get";
        private bool isViewRefreshing;



        public bool IsViewRefreshing
        {
            get=> isViewRefreshing;
            set
            {
                isViewRefreshing = value;
                OnPropertyChanged();
            }
        }
        public ICommand CommandNewsTapped { get; }
        public ICommand CommandScroll { get; }
        public ICommand CommandAppearing { get; }
        public ICommand CommandViewRefresh { get; }
        public ObservableCollection<NewsPost> NewsPosts { get; set; }

        private void OnAppearing(object param)
        {
            if (NewsPosts.Count != 0)
                return;
            IsViewRefreshing = true;
        }

        private async void NewsPageSelected(object param)
        {
            NewsPost selectedPost = param as NewsPost;
            await Application.Current.MainPage.Navigation.PushModalAsync(new PostPage(selectedPost));
        }

        private void OnScrolled(object param)
        {
            
        }

        private async void OnViewRefresh(object param)
        {
            NewsPosts.Clear();
            var newsPosts = await MakeRequest(methodTitle, paramDomain.Value, paramOffsetVal, paramCount.Value, paramFilter.Value);
            await foreach (var post in newsPosts)
                NewsPosts.Add(post);
            IsViewRefreshing = false;
        }
        private async Task<IAsyncEnumerable<NewsPost>> MakeRequest(string method, string domain,string offset, string count,string filter)
        {
            vkRequest = new VkRequest(method, new Dictionary<string, string>()
            {
                [paramDomain.Key] = domain,
                [paramOffsetKey] = offset,
                [paramCount.Key] = count,
                [paramFilter.Key] = filter
            });
            var response = await vkRequest.SendRequest(vkClient);
            var vkResponse = JsonConvert.DeserializeObject<VkResponse>(response);

            return vkResponse.ParseToNewsPost();
        }

    }
}
