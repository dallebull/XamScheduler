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
    public class YearIDToYearnameConverter : IValueConverter
    {

        private static ObservableCollection<Year> _allYears = new ObservableCollection<Year>();




        public ObservableCollection<Year> AllYears { get { return _allYears; } }


        public YearIDToYearnameConverter()
        {
            new YearViewModel();
        }

        public  object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
                      
            var yearname = from y in YearViewModel.AllYears where y.ID == (int)value select y.YearName;
            List<string> yearlist = yearname.ToList();
            if (yearlist.Count() > 0)
            {
                return yearlist.FirstOrDefault();
            }
            else return "Too Fast";
            
        }


   

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

      
    }
}
