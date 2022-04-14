using Xamarin.Forms;

namespace DemoHollywood.Models
{
    public class Document
    {
        public Document(string link,string fileName) 
        {
            Link = link;
            FileName = fileName;
        }

        public string Link { get; set; }
        public string FileName { get; set; }
    }
}
