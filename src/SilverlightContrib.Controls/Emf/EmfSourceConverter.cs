using System;
using System.ComponentModel;
using System.Globalization;

namespace SilverlightContrib.Controls
{
    /// <summary>
    /// Implements an EmfSource type converter.
    /// </summary>
    public class EmfSourceConverter : UriTypeConverter
    {
        /// <summary>
        /// Converts from a given value.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="value">The value.</param>
        /// <returns>The converted object.</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            // PNB: 9/29/2008 - Had to override ConvertFrom instead of ConvertFromString
            // since access modifiers have changed.

            string text = value as string;
            if ((text != null) || (value == null))
            {
                if (string.IsNullOrEmpty(text))
                {
                    return null;
                }
                return new EmfSource(new Uri(text, UriKind.RelativeOrAbsolute));
            }
            if (!(value is Uri))
            {
                throw new NotSupportedException(Resources.ExceptionConversionNotSupported);
            }
            return value;
        }

        // PNB: 9/29/2008 - Cannot override this method anymore.
        ///// <summary>
        ///// Converts from string.
        ///// </summary>
        ///// <param name="text">The text.</param>
        ///// <returns></returns>
        //public override object ConvertFromString(string text)
        //{
        //    if (string.IsNullOrEmpty(text))
        //    {
        //        return null;
        //    }
        //    return new Uri(text, UriKind.RelativeOrAbsolute);
        //}
    }
}
