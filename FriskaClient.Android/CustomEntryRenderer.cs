using Android.Content;
using FriskaClient.Android;
using FriskaClient.Behaviors;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace FriskaClient.Android
{
   

    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

   
            protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
            {
                base.OnElementChanged(e);        
          
                Control?.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
 
            }
    }
}

    //protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
    //{
    //    base.OnElementChanged(e);

    //    if (e.NewElement != null)
    //    {
    //        var view = (CustomEntry)Element;

    //        if (view.IsCurvedCornersEnabled)
    //        {
    //            // creating gradient drawable for the curved background
    //            var _gradientBackground = new GradientDrawable();
    //            _gradientBackground.SetShape(ShapeType.Rectangle);
    //            _gradientBackground.SetColor(view.NewBackgroundColor.ToAndroid());

    //            // Thickness of the stroke line
    //            _gradientBackground.SetStroke(view.BorderWidth, view.BorderColor.ToAndroid());

    //            // Radius for the curves
    //            _gradientBackground.SetCornerRadius(
    //                DpToPixels(this.Context,
    //                    Convert.ToSingle(view.CornerRadius)));

    //            // set the background of the label
    //            Control.SetBackground(_gradientBackground);
    //        }

    //        // Set padding for the internal text from border
    //        Control.SetPadding(
    //            (int)DpToPixels(this.Context, Convert.ToSingle(12)),
    //            Control.PaddingTop,
    //            (int)DpToPixels(this.Context, Convert.ToSingle(12)),
    //            Control.PaddingBottom);
    //    }
    //}
    //public static float DpToPixels(Context context, float valueInDp)
    //{
    //    DisplayMetrics metrics = context.Resources.DisplayMetrics;
    //    return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
    //}

