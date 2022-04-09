using System.Collections.Generic;

namespace DemoHollywood.Models.Vk.Post
{
    public class Response
    {
        public int count { get; set; }
        public IList<VkPost> items { get; set; }
    }
}
