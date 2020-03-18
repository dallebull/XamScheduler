using FriskaClient.Model;
using FriskaClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FriskaClient
{
    public class BoolToJaNejConverter : IValueConverter
    {





        public  object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Svar = "Nej";
            var Bool = (bool)value;
            if (Bool)
            {
                Svar = "Ja";
            }

            return Svar ;
        }


   

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
