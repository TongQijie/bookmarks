using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Bookmarks.Wpf.Converter
{
    class TreeViewLevel2LeadingConverter : IValueConverter
    {
        public int Level { get; set; }

        public double Leading { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var level = (int)value;
            return new Thickness(level * Leading, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
