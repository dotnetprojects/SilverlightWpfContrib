using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SilverlightContrib.Utilities;

namespace SilverlightContrib.Sample
{
    public partial class WheelMouseListenerSample : UserControl
    {
        public WheelMouseListenerSample()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(WheelMouseListener_Loaded);
        }

        void WheelMouseListener_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.WheelMouseListener wml = new Utilities.WheelMouseListener();
            wml.MouseWheelScroll += new WheelMouseHandler(wml_MouseWheelScroll);
        }

        void wml_MouseWheelScroll(WheelMouseEventArgs args)
        {
            string newItem = string.Format("{0}: Wheel Delta: {1}", DateTime.Now, args.Delta);
            lbDelta.Items.Insert(0, newItem);
        }
    }
}
