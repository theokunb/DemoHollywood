using System.Collections.Generic;

namespace DemoHollywood.Models.Vk.Post
{
    public class VkResponse
    {
        public Response response { get; set; }
        
        public List<NewsPost> ParseToNewsPost()
        {
            List<NewsPost> result = new List<NewsPost>();
            foreach(var element in response.items)
            {
                var text = element.text;
                var date = element.date;
                List<string> urls = new List<string>();
                foreach(var attachment in element.attachments)
                {
                    if (attachment.content.GetType().Equals(typeof(VkPhoto)))
                        urls.Add(attachment.content.GetUrl());
                    else if (attachment.content.GetType().Equals(typeof(VkVideo)))
                    {
                        urls.Add(attachment.content.GetUrl());
                    }
                }
                result.Add(new NewsPost(date, text, urls));
            }
            return result;
        }
    }
}
