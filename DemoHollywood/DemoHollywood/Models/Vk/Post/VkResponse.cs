using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DemoHollywood.Models.Vk.Post
{
    public class VkResponse
    {
        public Response response { get; set; }
        
        public async IAsyncEnumerable<NewsPost> ParseToNewsPost()
        {;
            foreach(var element in response.items)
            {
                var text = RemoveHyperlink(element.text);
                var date = element.date;
                List<string> urls = new List<string>();
                if (element.attachments != null)
                {
                    foreach (var attachment in element.attachments)
                    {
                        if (attachment.content.GetType().Equals(typeof(VkPhoto)))
                            urls.Add(attachment.content.GetUrl());
                        else if (attachment.content.GetType().Equals(typeof(VkVideo)))
                        {
                            urls.Add(attachment.content.GetUrl());
                        }
                    }
                }
                await Task.Delay(1);
                yield return new NewsPost(date, text, urls);
            }
        }

        private string RemoveHyperlink(string text)
        {
            Regex regex = new Regex(@"\[{1}\w+\|{1}(\w+)(\s+|\w+)*\]{1}");
            if (regex.IsMatch(text))
            {
                var parsedText = regex.Split(text);
                StringBuilder stringBuilder = new StringBuilder();
                foreach(var line in parsedText)
                {
                    stringBuilder.Append(line + " ");
                }
                text = stringBuilder.ToString();
            }
            return text;
        }

    }
}
