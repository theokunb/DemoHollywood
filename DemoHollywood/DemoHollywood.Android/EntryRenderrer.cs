using Android.Content;
using Android.Graphics.Drawables;
using DemoHollywood.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(EntryRenderrer))]
namespace DemoHollywood.Droid
{
    public class EntryRenderrer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Android.Graphics.Color.Transparent);
                Control.SetBackground(gd);
            }
        }
        public EntryRenderrer(Context context) : base(context) { }
    }
}