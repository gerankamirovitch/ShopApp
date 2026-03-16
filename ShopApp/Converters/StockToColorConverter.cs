using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace ShopApp.Converters
{
    public class StockGreaterThanZeroConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is int stock && stock > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}