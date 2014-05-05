using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using SilverlightContrib.Controls;

namespace SilverlightContrib.Sample
{
    public partial class EmfSample : UserControl
    {
        private string[] images = new string[] { "lion", "boat", "congress", "farm", "flower", "snake", "tool" };
        private int index = 0;

        public EmfSample()
        {
            InitializeComponent();
        }

        private void SetStretch(Stretch stretch)
        {
            if (this.emf != null) {
                this.emf.Stretch = stretch;
            }
        }

        private void ButtonNone_Checked(object sender, RoutedEventArgs e)
        {
            SetStretch(Stretch.None);
        }

        private void ButtonFill_Checked(object sender, RoutedEventArgs e)
        {
            SetStretch(Stretch.Fill);
        }

        private void ButtonUniform_Checked(object sender, RoutedEventArgs e)
        {
            SetStretch(Stretch.Uniform);
        }

        private void ButtonUniformToFill_Checked(object sender, RoutedEventArgs e)
        {
            SetStretch(Stretch.UniformToFill);
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog dlg = new OpenFileDialog();
            //dlg.Filter = "Metafiles (*.emf, *.wmf)|*.emf;*.wmf";
            //if (dlg.ShowDialog() == true) {
            //    this.txtFileName.Text = System.IO.Path.GetFileName(dlg.File.Name);
            //    using (Stream stream = dlg.File.OpenRead()) {
            //        emf.Source.SetSource(stream);
            //    }
            //}
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (++this.index == images.Length){
                this.index = 0;
            }

            Uri source = new Uri(string.Format("Images/Wmf/{0}.wmf", images[this.index]), UriKind.Relative);

            this.emf.Source.UriSource = source;
            this.txtFileName.Text = string.Format("{0}.wmf", images[this.index]);
        }
    }
}
