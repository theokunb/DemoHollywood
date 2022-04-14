using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DemoHollywood.Services
{
    public class VkClient
    {
        public VkClient()
        {
            httpClient = new HttpClient();
        }

        private const string AccessToken = "e80075d6e80075d6e80075d663e87c7e43ee800e80075d68a4e0dc9fc0c33127b47db47";
        private const string Version = "5.131";
        private readonly HttpClient httpClient;

        public async Task<string> ExecuteMethod(string method,Dictionary<string,string> param)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(var element in param)
            {
                stringBuilder.Append(element.Key + "=" + element.Value + "&");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);

            var response = await httpClient.GetAsync($"https://api.vk.com/method/{method}?{stringBuilder}&access_token={AccessToken}&v={Version}");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
