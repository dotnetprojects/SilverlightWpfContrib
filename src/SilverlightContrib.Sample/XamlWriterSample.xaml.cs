using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using SilverlightContrib.Controls;
using SilverlightContrib.Xaml;

namespace SilverlightContrib.Sample
{
    public partial class XamlWriterSample : UserControl
    {
        public XamlWriterSample()
        {
            InitializeComponent();

            this.list.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.list.SelectedItem == null) {
                return;
            }

            FrameworkElement element = null;

            if (this.list.SelectedIndex == 0) {
                element = this.button;
            }
            else if (this.list.SelectedIndex == 1) {
                element = this.emf;
            }
            else if (this.list.SelectedIndex == 2) {
                element = this.radioButton;
            }

            if (element != null) {
                StringBuilder b = new StringBuilder();
                XamlWriterSettings settings = new XamlWriterSettings();
                settings.WriteDefaultValues = this.checkDefault.IsChecked ?? false;

                using (XamlWriter writer = XamlWriter.CreateWriter(b, this.checkAttributes.IsChecked ?? false, settings)) {
                    writer.WriteElement(element);
                }

                result.Text = b.ToString();
            }
        }
    }
}
