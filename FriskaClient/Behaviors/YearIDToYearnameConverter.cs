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

        public  object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_allYears.Count == 0)
            {
                var vm = new YearViewModel();
                _allYears = vm.AllYears;

            }


            var yearname = from y in _allYears where y.ID == (int)value select y.YearName;
            List<string> yearlist = yearname.ToList();

            return yearlist.FirstOrDefault();
        }

   

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
