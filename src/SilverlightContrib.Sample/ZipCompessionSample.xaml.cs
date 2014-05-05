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
using System.Windows.Resources;
using System.Windows.Shapes;
using SilverlightContrib.IO.Compression.Zip;
using System.Text;

namespace SilverlightContrib.Sample
{
    public partial class ZipCompessionSample : UserControl
    {
        public ZipCompessionSample()
        {
            InitializeComponent();
            RadioButtonCreateZip.Checked += new RoutedEventHandler(RadioButtonCreateZip_Checked);
            RadioButtonExtractZip.Checked += new RoutedEventHandler(RadioButtonExtractZip_Checked);
        }

        void RadioButtonExtractZip_Checked(object sender, RoutedEventArgs e)
        {
            lbItems.Items.Clear();
            result.Text = "";

            StreamResourceInfo sr = Application.GetResourceStream(new Uri("Misc/testfile1.zip", UriKind.Relative));
            using (ZipInputStream zipInputStream = new ZipInputStream(sr.Stream))
            {
                ZipEntry entry = zipInputStream.GetNextEntry();
                while (entry != null)
                {

                    byte[] buffer = new byte[2048];
                    int bytesRead = 0;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        while ((bytesRead = zipInputStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            ms.Write(buffer, 0, bytesRead);
                        }
                        ms.Position = 0;

                        lbItems.Items.Add(entry.Name + " - " + ms.Length);

                        DisplayFile(ms);
                    }

                    entry = zipInputStream.GetNextEntry();
                }
            }
        }

        void RadioButtonCreateZip_Checked(object sender, RoutedEventArgs e)
        {
            lbItems.Items.Clear();
            result.Text = "";
            lbItems.Items.Add(new Uri("Misc/testfile1.txt", UriKind.Relative));
            lbItems.Items.Add(new Uri("Misc/cd_catalog.xml", UriKind.Relative));
            lbItems.MouseLeftButtonUp += new MouseButtonEventHandler(lbItems_MouseLeftButtonUp);
        }


        private void DisplayFile(MemoryStream ms)
        {
            using (StreamReader sr = new StreamReader(ms))
            {
                string data = 
                    result.Text = sr.ReadToEnd();
            }
        }

        void lbItems_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(RadioButtonCreateZip.IsChecked.Value)
                CompressFile((Uri)((ListBox)sender).SelectedItem);
        }

        private void CompressFile(Uri resourceUri)
        {
            if(resourceUri == null)
                throw (new ArgumentException("The resourceUri parameter value cannot be null"));
            if (resourceUri.ToString().Length == 0)
                throw (new IndexOutOfRangeException());

            result.Text = "";

            string filename = GetFilenameFromUri(resourceUri);

            // A memory stream to write to  
            using (MemoryStream zippedMemoryStream = new MemoryStream())
            {
                // A ZIP stream  
                using (ZipOutputStream zipOutputStream = new ZipOutputStream(zippedMemoryStream))
                {
                    using (Stream stream = App.GetResourceStream(resourceUri).Stream)
                    {
                        // Highest compression rating  
                        zipOutputStream.SetLevel(9);

                        // Show filename + length
                        result.Text += string.Format("{0}:{1}\n", filename, stream.Length);
                        byte[] buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);

                        // Write the data to the ZIP file              
                        ZipEntry entry = new ZipEntry(string.Format("{0}.zip", filename));
                        zipOutputStream.PutNextEntry(entry);
                        zipOutputStream.Write(buffer, 0, buffer.Length);
                        zipOutputStream.Finish();

                        // Show Zipped filename + length
                        result.Text += string.Format("{0}.zip:{1}\n", filename, zipOutputStream.Length);
                    }
                }
            }
        }

        private static string GetFilenameFromUri(Uri resourceUri)
        {
            string filename = resourceUri.ToString();
            if (resourceUri.ToString().IndexOf('/') > -1)
            {
                string[] uriParts = filename.Split('/');
                if (uriParts.Count() > 0)
                    filename = uriParts[uriParts.Count() - 1];
            }
            return filename;
        }
    }
}
