using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace FriskaClient
{
    public class MsgToImagePathConverter : IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imagePath = "ms-appx:///Assets/1.png";
            var Rattsvar = (bool)value;
            if (Rattsvar)
            {
                imagePath = "ms-appx:///Assets/2.png";
            }

            return imagePath;
        }

   

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
