using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace DemoHollywood.Models
{
    public class NewsPost
    {
        public NewsPost(int date,string text, List<string> urls)
        {
            this.date = ConvertUnixDate(date);
            this.text = text;
            Images = new ObservableCollection<ImageSource>();
            foreach(var element in urls)
            {
                Images.Add(ImageSource.FromUri(new Uri(element)));
            }
        }

        private string date;
        private string text;


        public string Date => date;
        public string Text => text;
        public ObservableCollection<ImageSource> Images { get; set; }

        private string ConvertUnixDate(int date)
        {
            DateTime convertedDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            convertedDate = convertedDate.AddSeconds(date).ToLocalTime();
            return convertedDate.ToShortDateString();
        }
    }
}
