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
using System.Windows.Markup;

using SilverlightContrib.Xaml.Rtf;

namespace SilverlightContrib.Sample
{
    public partial class RtfSample : UserControl
    {
        public RtfSample()
        {
            InitializeComponent();

            this.rtf.Text = @"{\rtf1\ansi\ansicpg\lang1024\noproof65001\uc1 \deff0{\fonttbl{\f0\fnil\fcharset0\fprq1 Courier New;}}{\colortbl;
\red0\green0\blue255;\red255\green255\blue255;\red163\green21\blue21;\red255\green0\blue0;\red0\green0\blue0;}
\fs16 \cf1 <\cf3 UserControl\cf4  x\cf1 :\cf4 Class\cf1 =""SilverlightContrib.Sample.Rtf""\par 
\cf0    \cf4  xmlns\cf1 =""http://schemas.microsoft.com/winfx/2006/xaml/presentation""\cf0  \par 
   \cf4  xmlns\cf1 :\cf4 x\cf1 =""http://schemas.microsoft.com/winfx/2006/xaml"">\par 
\cf3     \cf1 <\cf3 StackPanel\cf4  x\cf1 :\cf4 Name\cf1 =""LayoutRoot""\cf4  Background\cf1 =""White"">\par 
\cf3         \cf1 <\cf3 TextBox\cf4  x\cf1 :\cf4 Name\cf1 =""rtf""\cf4  Height\cf1 =""300""\cf4  AcceptsReturn\cf1 =""True""></\cf3 TextBox\cf1 >\par 
\cf3         \cf1 <\cf3 Button\cf4  Content\cf1 =""Convert""\cf4  Click\cf1 =""Button_Click"" />\par 
\cf3         \cf1 <\cf3 Grid\cf4  x\cf1 :\cf4 Name\cf1 =""result"">\par 
\cf3             \par 
        \cf1 </\cf3 Grid\cf1 >\par 
\cf3     \cf1 </\cf3 StackPanel\cf1 >\par 
</\cf3 UserControl\cf1 >}
";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RtfConverter converter = new RtfConverter();

            this.result.Child = (UIElement)XamlReader.Load(converter.ToXaml(rtf.Text));
        }
    }
}
