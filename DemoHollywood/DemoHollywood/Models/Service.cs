using System.Text;

namespace DemoHollywood.Models
{
    public class Service
    {
        public Service() { }

        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public short Duration { get; set; }
        public int Price { get; set; }


        public string DisplayTitle => $"Услуга: {Title}";
        public string DisplayPrice => $"Стоимость: {Price}";
        public string DisplayDuration
        {
            get
            {
                short inTime = (short)(Duration * 30);
                StringBuilder sb = new StringBuilder();
                sb.Append("Продолжительнось приёма:");
                if (inTime / 60 > 0)
                {
                    sb.Append($" {inTime / 60} ч.");
                }
                if (inTime % 60 > 0)
                {
                    sb.Append($" {inTime % 60} мин.");
                }
                return sb.ToString();
            }
        }

        public bool IsValid()
        {
            return !(string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(Description) && string.IsNullOrEmpty(ImagePath) && string.IsNullOrEmpty(ImageName) && Duration > 0 && Price > 0);
        }
    }
}