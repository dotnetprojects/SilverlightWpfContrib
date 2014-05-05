using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightContrib.Test.Data.Converters
{
    public class CustomControl : Control
    {
        [TypeConverter(typeof(SilverlightContrib.Data.Converters.CharTypeConverter))]
        public char MyProperty
        {
            get { return (char)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        public static readonly DependencyProperty MyPropertyProperty =
              DependencyProperty.Register(
              "MyProperty", typeof(char), typeof(CustomControl), null);
    }
}
