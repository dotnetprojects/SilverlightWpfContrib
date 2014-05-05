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
    public class StarSelectorTest : SilverlightControlTest
    {
        [TestInitialize]
        public void Setup()
        {
            
        }

        [TestMethod]
        [Description("Create a StarSelector control")]
        public void CreateStarSelector()
        {
            StarSelector ss = new StarSelector();
            Assert.IsNotNull(ss);
        }

        [TestMethod]
        [Description("Verify the default SelectedRating")]
        [Asynchronous]
        public void DefaultRating()
        {
            StarSelector ss = new StarSelector();
            CreateAsyncTest(ss, () => Assert.AreEqual(0, ss.SelectedRating));
        }

        [TestMethod]
        [Description("Verify that SelectedRating is set correctly")]
        [Asynchronous]
        public void SelectedRating()
        {
            StarSelector ss = new StarSelector {SelectedRating = 1};
            CreateAsyncTest(ss, () => Assert.AreEqual(1, ss.SelectedRating));
        }

        [TestMethod]
        [Description("Verify that ReadOnly is set correctly")]
        [Asynchronous]
        public void ReadOnly()
        {
            StarSelector ss = new StarSelector { ReadOnly = true };
            CreateAsyncTest(ss, () => Assert.AreEqual(true, ss.ReadOnly));
        }

        [TestMethod]
        [Description("Ensure RatingChanged event is fired")]
        public void RatingChangedEvent()
        {
            StarSelector ss = new StarSelector();
            ss.RatingChanged += new RatingChangedEventHandler(
                (sender, e) => Assert.AreEqual(3, e.Rating));
            ss.SelectedRating = 3;
            Assert.AreEqual(3, ss.SelectedRating);
        }


    }
}
