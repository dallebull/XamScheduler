using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using FriskaClient.Behaviors;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace FriskaClient.iOS
{

    public class CustomEntryRenderer : EntryRenderer
    {
     
            protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
            {
                base.OnElementChanged(e);

                if (Control != null)
                {

                    Control.BorderStyle = UITextBorderStyle.None;
                    Control.Layer.CornerRadius = 10;
                    Control.TextColor = UIColor.White;

                }
            }
        //    protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        //{
        //    base.OnElementChanged(e);

        //    if (e.NewElement != null)
        //    {
        //        var view = (CustomEntry)Element;

        //        Control.LeftView = new UIView(new CGRect(0f, 0f, 9f, 20f));
        //        Control.LeftViewMode = UITextFieldViewMode.Always;
        //        Control.KeyboardAppearance = UIKeyboardAppearance.Dark;
        //        Control.ReturnKeyType = UIReturnKeyType.Done;
        //        Control.Layer.CornerRadius = Convert.ToSingle(view.CornerRadius);
        //        Control.Layer.BorderColor = view.BorderColor.ToCGColor();
        //        Control.Layer.BorderWidth = view.BorderWidth;
        //        Control.ClipsToBounds = true;
        //    }
        //}
    }
}