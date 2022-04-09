using System;
using System.Collections.Generic;
using System.Text;

namespace DemoHollywood.Models.Vk.Post
{
    public class VkLink : IAttachment
    {
        public string url { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string target { get; set; }
        public VkPhoto photo { get; set; }

        public string GetUrl()
        {
            return url;
        }
    }
}
