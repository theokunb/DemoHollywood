using System.Collections.Generic;

namespace DemoHollywood.Models.Vk.Post
{
    public class VkAudio : IAttachment
    {
        public string artist { get; set; }
        public int id { get; set; }
        public int owner_id { get; set; }
        public string title { get; set; }
        public int duration { get; set; }
        public bool is_explicit { get; set; }
        public bool is_focus_track { get; set; }
        public string track_code { get; set; }
        public string url { get; set; }
        public int date { get; set; }
        public IList<Artist> main_artists { get; set; }
        public bool short_videos_allowed { get; set; }
        public bool stories_allowed { get; set; }
        public bool stories_cover_allowed { get; set; }

        public string GetUrl()
        {
            return url;
        }
    }
}
