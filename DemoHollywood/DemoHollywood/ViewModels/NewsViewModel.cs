using Acr.UserDialogs;
using DemoHollywood.Models;
using DemoHollywood.Models.Vk;
using DemoHollywood.Models.Vk.Post;
using DemoHollywood.Services;
using DemoHollywood.Views;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DemoHollywood.ViewModels
{
    public class NewsViewModel : BaseViewModel
    {
        public NewsViewModel()
        {
            vkClient = new VkClient();
            paramOffsetVal = "0";
            NewsPosts = new ObservableCollection<NewsPost>();

            CommandNewsTapped = new Command((param) => NewsPageSelected(param));
            CommandScroll = new Command((param) => OnScrolled(param));
            CommandAppearing = new Command((param) => OnAppearing(param));
        }


        private VkClient vkClient;
        private string paramOffsetVal;
        private VkResponse vkResponse;
        private static KeyValuePair<string, string> paramDomain = new KeyValuePair<string, string>("domain", "hollywood_dental_ykt");
        private static KeyValuePair<string, string> paramCount = new KeyValuePair<string, string>("count", "10");
        private static KeyValuePair<string, string> paramFilter = new KeyValuePair<string, string>("filter", "owner");
        private const string paramOffsetKey = "offset";
        private const string methodTitle = "wall.get";


        public ICommand CommandNewsTapped { get; }
        public ICommand CommandScroll { get; }
        public ICommand CommandAppearing { get; }
        public ObservableCollection<NewsPost> NewsPosts { get; set; }

        private async void OnAppearing(object param)
        {
            if (NewsPosts.Count != 0)
                return;
            VkRequest vkRequest = new VkRequest(methodTitle, new Dictionary<string, string>()
            {
                [paramDomain.Key] = paramDomain.Value,
                [paramOffsetKey] = paramOffsetVal,
                [paramCount.Key] = paramCount.Value,
                [paramFilter.Key] = paramFilter.Value
            });
            var response = await vkRequest.SendRequest(vkClient);
            vkResponse = JsonConvert.DeserializeObject<VkResponse>(response);

            var newsPosts = vkResponse.ParseToNewsPost();
            foreach (var post in newsPosts)
                NewsPosts.Add(post);
        }

        private async void NewsPageSelected(object param)
        {
            NewsPost selectedPost = param as NewsPost;
            await Application.Current.MainPage.Navigation.PushModalAsync(new PostPage(selectedPost));
        }
        private void OnScrolled(object param)
        {
            
        }
    }
}
