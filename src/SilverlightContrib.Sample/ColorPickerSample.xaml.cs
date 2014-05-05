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
using SilverlightContrib.Controls;

namespace SilverlightContrib.Sample
{
    public partial class ColorPickerSample : UserControl
    {
        private readonly Random m_rand;

        public ColorPickerSample()
        {
            InitializeComponent();
            m_rand = new Random();

            UpdateCurrentColor();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            byte[] buff = new byte[3];
            m_rand.NextBytes(buff);
            ColorPicker1.SelectedColor = Color.FromArgb(255, buff[0], buff[1], buff[2]);
            UpdateCurrentColor();
        }

        private void UpdateCurrentColor()
        {
            if (tbCurrColor != null) {
                tbCurrColor.Text = string.Format("(Changed Event) Selected color: {0}", ColorPicker1.SelectedColor);
            }
        }

        private void ColorPicker1_SelectedColorChanged(object sender, SelectedColorEventArgs args)
        {
            UpdateCurrentColor();
        }

        private void ColorPicker1_SelectedColorChanging(object sender, SelectedColorEventArgs e)
        {
            tbCurrColor1.Text = string.Format("(Changing Event) Selected color: {0}", e.SelectedColor);
        }

    }
}
