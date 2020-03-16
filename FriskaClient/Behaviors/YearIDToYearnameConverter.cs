using FriskaClient.Model;
using FriskaClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FriskaClient
{
    public class YearIDToYearnameConverter : IValueConverter
    {

        private ObservableCollection<Year> _allYears = new ObservableCollection<Year>();


        public ObservableCollection<Year> AllYears { get { return _allYears; } }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sv = new YearViewModel();
             _allYears= sv.AllYears;

            var yearname =from y in _allYears where y.YearID == (int)value select y.YearName;


            return yearname;
        }

   

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
