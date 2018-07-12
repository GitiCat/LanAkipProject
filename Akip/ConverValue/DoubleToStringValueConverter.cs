using System;
using System.Globalization;

namespace Akip
{
    public class DoubleToStringValueConverter : BaseValueConverter<DoubleToStringValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null) {
                return value.ToString();
            } else {
                return "";
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
