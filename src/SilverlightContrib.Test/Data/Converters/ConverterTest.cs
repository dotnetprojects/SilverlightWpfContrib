using System;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Markup;

namespace SilverlightContrib.Test.Data.Converters
{
    public class ConverterTests
    {   
        [TestClass]
        public class CharConverter : PresentationTest
        {
            [TestInitialize]
            public void Setup()
            {

            }

            [TestMethod]
            [Description("Instantiate a control through XAML with a char dependency property.")]
            public void CreateControlWithCharDependencyProperty()
            {

                var a = XamlReader.Load(@"<loc:CustomControl 
                                            xmlns:loc=""clr-namespace:SilverlightContrib.Test.Data.Converters;assembly=SilverlightContrib.Test"" 
                                            xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                                            MyProperty=""B""></loc:CustomControl>") as CustomControl;
                Assert.AreEqual('B', a.MyProperty);

                var b = XamlReader.Load(@"<loc:CustomControl 
                                            xmlns:loc=""clr-namespace:SilverlightContrib.Test.Data.Converters;assembly=SilverlightContrib.Test"" 
                                            xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                                            MyProperty=""123H""></loc:CustomControl>") as CustomControl;
                Assert.AreEqual('1', b.MyProperty);

                b.MyProperty = 'Q';

                Assert.AreEqual('Q', b.MyProperty);
            }

        }
    }
}
