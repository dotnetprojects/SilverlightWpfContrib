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
using SilverlightContrib.Utilities.ClipboardHelper;

namespace SilverlightContrib.Sample
{
    public partial class ClipboardHelperSample : UserControl
    {
        ClipboardHelper clipboardHelper;

        public ClipboardHelperSample()
        {
            InitializeComponent();
            ButtonSetClipboardText.Click += new RoutedEventHandler(ButtonSetClipboardText_Click);
            ButtonGetClipboardText.Click += new RoutedEventHandler(ButtonGetClipboardText_Click);
            ButtonClearClipboard.Click += new RoutedEventHandler(ButtonClearClipboard_Click);

            this.Loaded += new RoutedEventHandler(ClipboardHelperSample_Loaded);
        }

        void ClipboardHelperSample_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                clipboardHelper = new ClipboardHelper(TextBoxInput, ButtonTest);
            }
            catch (InvalidOperationException)
            {
                TextBlockOutput.Text = "Sorry, clipboard support not supported in current browser";
            }
        }


        void ButtonClearClipboard_Click(object sender, RoutedEventArgs e)
        {
            clipboardHelper.ClearData();
        }

        void ButtonGetClipboardText_Click(object sender, RoutedEventArgs e)
        {
            string text = clipboardHelper.GetData();
            TextBlockOutput.Text = text;
        }

        void ButtonSetClipboardText_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxInput.Text.Length > 0)
            {
                clipboardHelper.SetData(TextBoxInput.Text);
            }
        }
    }
}
