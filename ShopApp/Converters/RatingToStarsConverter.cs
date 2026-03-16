using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace ShopApp.Converters
{
    public class RatingToStarsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double rating)
            {
                int fullStars = (int)Math.Round(rating);
                return new string('★', fullStars) + new string('☆', 5 - fullStars);
            }
            return "☆☆☆☆☆";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}