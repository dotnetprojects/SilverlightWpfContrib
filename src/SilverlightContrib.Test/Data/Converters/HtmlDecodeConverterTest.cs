using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SilverlightContrib.Data.Converters;
using System.Diagnostics;
using Microsoft.Silverlight.Testing;

namespace SilverlightContrib.Test.Data.Converters
{
    [TestClass]
    public class HtmlDecodeConverterTest
    {
        private HtmlDecodeConverter m_converter;

        [TestInitialize]
        public void Setup()
        {
            m_converter = new HtmlDecodeConverter(); 
        }

        [TestMethod]
        [Description("Attempt to decode a trademark character entity and friendly copyright entity.")]
        public void HtmlDecoderTest()
        {
            string tradeMark = m_converter.Convert("&#8482;", typeof(string), null, null) as string;
            Assert.AreEqual("™", tradeMark);

            string copyright = m_converter.Convert("&copy;", typeof(string), null, null) as string;
            Assert.AreEqual("©", copyright);
        }

        [TestMethod]
        public void EmptyAndNullValues()
        {
            string result = m_converter.Convert("", typeof(string), null, null) as string;
            Assert.AreEqual(string.Empty, result);

            result = m_converter.Convert(null, typeof(string), null, null) as string;
            Assert.AreEqual(null, result);
        }
    }
}
