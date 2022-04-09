namespace DemoHollywood.Models.Vk.Post
{
    public class VkDoc : IAttachment
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public string title { get; set; }
        public int size { get; set; }
        public string ext { get; set; }
        public string date { get; set; }
        public int type { get; set; }
        public string url { get; set; }
        public string access_key { get; set; }

        public string GetUrl()
        {
            return url;
        }
    }
}
