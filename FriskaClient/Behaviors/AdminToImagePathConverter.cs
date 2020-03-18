using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace FriskaClient
{
    public class AdminToImagePathConverter : IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var imagePath = "";
            var IsAdmin = (bool)value;
            if (IsAdmin)
            {
                imagePath = "Admin.png";
            }

            return imagePath;
        }

   

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
