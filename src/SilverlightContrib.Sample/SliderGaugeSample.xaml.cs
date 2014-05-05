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

namespace SilverlightContrib.Sample
{
    public partial class SliderGaugeSample : UserControl
    {
        public SliderGaugeSample()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(SliderGaugeSample_Loaded);
        }

        void SliderGaugeSample_Loaded(object sender, RoutedEventArgs e)
        {
            txtPercent1.Text = slider1.Percentage.ToString("p");
            txtPercent2.Text = slider2.Percentage.ToString("p");
            txtPercent3.Text = slider3.Percentage.ToString("p");
        }


        private void SliderGauge_PercentChanged1(object sender, SilverlightContrib.Controls.GaugePercentageChangedEventArgs e)
        {
            txtPercent1.Text = e.Percentage.ToString("p");
        }

        private void SliderGauge_PercentChanged2(object sender, SilverlightContrib.Controls.GaugePercentageChangedEventArgs e)
        {
            txtPercent2.Text = e.Percentage.ToString("p");
        }

        private void SliderGauge_PercentChanged3(object sender, SilverlightContrib.Controls.GaugePercentageChangedEventArgs e)
        {
           txtPercent3.Text = e.Percentage.ToString("p");
        }
    }
}
