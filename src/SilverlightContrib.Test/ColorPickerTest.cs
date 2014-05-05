using System;
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
using SilverlightContrib.Controls;

namespace SilverlightContrib.Test
{
    [TestClass]
    public class ColorPickerTest : PresentationTest
    {
        [TestInitialize]
        public void Setup()
        {

        }

        [TestMethod]
        [Description("Create a ColorPicker control")]
        public void CreateStarSelector()
        {
            ColorPicker cp = new ColorPicker();
            TestPanel.Children.Add(cp);
        }


        [TestMethod]
        public void RgbHsvConversion()
        {
            ColorSpace cs = new ColorSpace();
            for(int r = 0; r<255; r+=5)
                for(int g=0; g<255; g+=5)
                    for(int b=0; b<255; b+=5)
                    {
                        Color c1 = Color.FromArgb(255, (byte)r, (byte)g, (byte)b);
                        HSV tmpHSL = cs.ConvertRgbToHsv(c1);
                        Color c2 = cs.ConvertHsvToRgb(tmpHSL.Hue, tmpHSL.Saturation, tmpHSL.Value);
                        Assert.AreEqual(c1.R, c2.R);
                        Assert.AreEqual(c1.G, c2.G);
                        Assert.AreEqual(c1.B, c2.B);
                        Assert.AreEqual(c1.A, c2.A);   
                    }
        }
    }
}
