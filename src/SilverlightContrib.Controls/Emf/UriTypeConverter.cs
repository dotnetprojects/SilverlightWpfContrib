using System;
using System.ComponentModel;

namespace SilverlightContrib.Controls
{
    /// <summary>
    /// Implements a Uri type converter.
    /// </summary>
    public class UriTypeConverter : TypeConverter
    {
        /// <summary>
        /// Determines whether this instance [can convert from] the specified source type.
        /// </summary>
        /// <param name="sourceType">Type of the source.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// 	<c>true</c> if this instance [can convert from] the specified source type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == null)
            {
                throw new ArgumentNullException("sourceType");
            }
            if (sourceType != typeof(string))
            {
                return typeof(Uri).IsAssignableFrom(sourceType);
            }
            return true;
        }

        /// <summary>
        /// Converts from given value.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="value">The value.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>Returns the converted object.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            string text = value as string;
            if ((text != null) || (value == null))
            {
                return ConvertFromString(text);
            }
            if (!(value is Uri))
            {
                throw new NotSupportedException(Resources.ExceptionConversionNotSupported);
            }
            return value;
        }
    }
}
