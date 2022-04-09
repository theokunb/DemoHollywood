using System;
using System.Collections.Generic;
using System.Text;

namespace DemoHollywood.Models.Vk.Post
{
    public class VkVideo : IAttachment
    {
        public string access_key { get; set; }
        public int can_comment { get; set; }

        public int id { get; set; }
        public int owner_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int duration { get; set; }
        public IList<VkImage> image { get; set; }
        public IList<VkFrame> first_frame { get; set; }
        public int date { get; set; }
        public int adding_date { get; set; }
        public int views { get; set; }
        public int local_views { get; set; }
        public int comments { get; set; }
        public string player { get; set; }
        public string platform { get; set; }
        public int can_add { get; set; }
        public int is_private { get; set; }
        public int processing { get; set; }
        public bool is_favorite { get; set; }
        public int can_edit { get; set; }
        public int can_like { get; set; }
        public int can_repost { get; set; }
        public int can_subscribe { get; set; }
        public int can_add_to_faves { get; set; }
        public int can_attach_link { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int user_id { get; set; }
        public int converting { get; set; }
        public int added { get; set; }
        public int is_subscribed { get; set; }
        public int repeat { get; set; }
        public string type { get; set; }
        public int balance { get; set; }
        public string live_status { get; set; }
        public int live { get; set; }
        public int upcoming { get; set; }
        public int spectators { get; set; }
        public LikeVideo likes { get; set; }
        public RepostVideo reposts { get; set; }
        public string track_code { get; set; }

        public string GetUrl()
        {
            return image[image.Count/2].url;
        }
    }
}
