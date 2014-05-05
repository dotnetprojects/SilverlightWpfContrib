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
    public partial class ConverterSamples : UserControl
    {
        public ConverterSamples()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ConverterSamples_Loaded);
        }

        void ConverterSamples_Loaded(object sender, RoutedEventArgs e)
        {
            var p = from x in Enumerable.Range(161, 10)
                    select new EntityDescription { Entity = string.Format("&#{0};", x) };
            lstItemsHtmlDecoder.ItemsSource = p;

            var p2 = from x in Enumerable.Range(0, 30)
                     select new EntityDescription {EntityDate = DateTime.Now.AddDays(x)};
            lstItemsStringFormatter.ItemsSource = p2;

            var p3 = from x in Enumerable.Range(10, 30)
                     select new EntityDescription { Price = ((decimal)x) / 2 };
            lstItemsMoneyFormatter.ItemsSource = p3;

            var p4 = from x in Enumerable.Range(0, 30)
                     select new EntityDescription { EntityDate = DateTime.Now.AddDays(x) };
            this.lstItemsDateTimeConverter.ItemsSource = p4;
        }


    }

    public class EntityDescription
    {
        public string Entity 
        {
            get; set;
        }

        public DateTime EntityDate
        {
            get; set;
        }

        public decimal Price
        {
            get; set;
        }
    }
}
