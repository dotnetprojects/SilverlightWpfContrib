using System;
using System.Globalization;
using System.Windows.Data;

namespace SilverlightContrib.Data.Converters
{
    /// <summary>
    /// Used to format strings during databinding.
    /// </summary>
    public class StringFormatConverter : IValueConverter
    {
        /// <summary>
        /// Called during data binding.  Formats strings with the provided format string.
        /// </summary>
        /// <param name="value">The string to format.</param>
        /// <param name="targetType">Not used, should be a string.</param>
        /// <param name="parameter">The format string.</param>
        /// <param name="culture">The culture to use when formatting.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
                return null;

            string format = parameter as string;
            return string.IsNullOrEmpty(format) ? value.ToString() : string.Format(culture, format, value);
        }

        /// <summary>
        /// Not implemented. 
        /// </summary>
        /// <param name="value">Not implemented.</param>
        /// <param name="targetType">Not implemented.</param>
        /// <param name="parameter">Not implemented.</param>
        /// <param name="culture">Not implemented.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
