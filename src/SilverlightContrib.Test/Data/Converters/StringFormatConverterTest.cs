using System;
using System.Windows.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SilverlightContrib.Data.Converters;

namespace SilverlightContrib.Test.Data.Converters
{
    [TestClass]
    public class StringFormatConverterTest
    {
        private StringFormatConverter m_converter;

        [TestInitialize]
        public void Setup()
        {
            m_converter = new StringFormatConverter(); 
        }

        [TestMethod]
        [Description("Attempt to convert a date to a short date string.")]
        public void FormatDateTest()
        {
            DateTime currDate = DateTime.Now;
            string shortDateString = m_converter.Convert(currDate, typeof(string), "{0:MM/dd/yyyy}", null) as string;
            Assert.AreEqual(currDate.ToString("MM/dd/yyyy"), shortDateString);
        }

        [TestMethod]
        [Description("Attempt to convert a double to a percentage.")]
        public void FormatPercentageTest()
        {
            double x = 0.786;
            string percentage = m_converter.Convert(x, typeof(string), "{0:p0}", null) as string;
            Assert.AreEqual(x.ToString("p0"), percentage);
        }

        [TestMethod]
        [Description("Test return value for empty and null format strings.")]
        public void EmptyAndNullFormatter()
        {
            double x = 0.3;
            string percentage = m_converter.Convert(x, typeof(string), string.Empty, null) as string;
            Assert.AreEqual(x.ToString(), percentage);

            percentage = m_converter.Convert(x, typeof(string), null, null) as string;
            Assert.AreEqual(x.ToString(), percentage);
        }
    }

}
