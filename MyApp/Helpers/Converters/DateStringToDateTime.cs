using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MyApp.Helpers.Converters
{
    public class DateStringToDateTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime outValue = new DateTime();
            if (value != null)
            {

                outValue = DateTime.ParseExact((string)value,
                                 "ddd, dd MMM yyyy HH:mm:ss zzz", null);
            }

            return outValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
