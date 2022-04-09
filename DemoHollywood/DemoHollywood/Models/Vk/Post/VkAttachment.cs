namespace DemoHollywood.Models.Vk.Post
{
    public class VkAttachment
    {
        public string type { get; set; }
        public VkPhoto photo { get; set; }
        public VkDoc doc { get; set; }
        public VkAudio audio { get; set; }
        public VkLink link { get; set; }
        public VkVideo video { get; set; }

        public IAttachment content
        {
            get
            {
                if (type.Equals(nameof(photo)))
                    return photo;
                else if (type.Equals(nameof(doc)))
                    return doc;
                else if (type.Equals(nameof(audio)))
                    return audio;
                else if (type.Equals(nameof(link)))
                    return link;
                else if (type.Equals(nameof(video)))
                    return video;
                else return null;
            }
        }
    }
}
