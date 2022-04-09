using System.Collections.Generic;

namespace DemoHollywood.Models.Vk.Post
{
    public class VkPost
    {
        public int id { get; set; }
        public int from_id { get; set; }
        public int owner_id { get; set; }
        public int date { get; set; }
        public int marked_as_ads { get; set; }
        public string post_type { get; set; }
        public string text { get; set; }
        public int is_pinned { get; set; }
        public IList<VkAttachment> attachments { get; set; }
        public PostSource post_source { get; set; }
        public Comment comments { get; set; }
        public Like likes { get; set; }
        public Repost reposts { get; set; }
        public View views { get; set; }
        public Donut donut { get; set; }
        public double short_text_rate { get; set; }
        public int carousel_offset { get; set; }
        public int edited { get; set; }
        public string hash { get; set; }
    }
}
