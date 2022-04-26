using Android.Content;
using Android.Graphics.Drawables;
using DemoHollywood.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Editor), typeof(EditorRenderrer))]
namespace DemoHollywood.Droid
{
    public class EditorRenderrer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if(Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Android.Graphics.Color.Transparent);
                Control.SetBackground(gd);
            }
        }

        public EditorRenderrer(Context context) : base(context) { }
    }
}