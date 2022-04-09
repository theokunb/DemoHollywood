using System.Collections.Generic;

namespace DemoHollywood.Models.Vk.Post
{
    public class VkPhoto : IAttachment
    {
        public int album_id { get; set; }
        public int date { get; set; }
        public int id { get; set; }
        public int owner_id { get; set; }
        public string access_key { get; set; }
        public IList<VkSize> sizes { get; set; }
        public string text { get; set; }
        public bool has_tags { get; set; }
        public int user_id { get; set; }

        public string GetUrl()
        {
            if (sizes != null)
                return sizes[sizes.Count/2].url;
            else return string.Empty;
        }
    }
}
