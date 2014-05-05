using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SilverlightContrib.Sample
{
    public partial class Page : UserControl
    {
        private readonly SampleItem[] samples = new[]{
            new SampleItem(){
                Title = "Color Picker",
                Description = "The resizeable Color Picker control allows a user to select a color.",
                CreateSample = new SampleFactory(() => new ColorPickerSample())
            },
            new SampleItem(){
                Title = "Cool Menu",
                Description = "Cool Menu similar to the Dock Bar in Mac OS.",
                CreateSample = new SampleFactory(() => new CoolMenuSample())
            },
            new SampleItem(){
                Title = "Enhanced Metafile",
                Description = "The EMF control adds support for Windows Metafile images to Silverlight.",
                CreateSample = new SampleFactory(() => new EmfSample())
            },
            new SampleItem(){
                Title = "Rich Text Format",
                Description = "Convert Rich Text Format data (RTF) to XAML.",
                CreateSample = new SampleFactory(() => new RtfSample())
            },
            new SampleItem(){
                Title = "Slider Gauge",
                Description = "Slider Gauge for allowing users to select a percentage.",
                CreateSample = new SampleFactory(() => new SliderGaugeSample())
            },
            new SampleItem(){
                Title = "Star Selector",
                Description = "A Star Selector control demonstrating various custom templates.",
                CreateSample = new SampleFactory(() => new StarSelectorSample())
            },
            new SampleItem(){
                Title = "Tweening",
                Description = "Adding Tweening animations to your application is super easy using the declarative Tweening framework of Silverlight Contrib.",
                CreateSample = new SampleFactory(() => new TweeningSample())
            },
            new SampleItem(){
                Title = "Mouse Wheel",
                Description = "Mouse Wheel event support. Move your mouse wheel.",
                CreateSample = new SampleFactory(() => new WheelMouseListenerSample())
            },
            new SampleItem(){
                Title = "XAML Writer",
                Description = "Generate XAML from any live Silverlight object model.",
                CreateSample = new SampleFactory(() => new XamlWriterSample())
            },
            new SampleItem(){
                Title = "ZIP Compression",
                Description = "A Silverlight port of the SharpZib library.",
                CreateSample = new SampleFactory(() => new ZipCompessionSample())
            },
            new SampleItem(){
                Title = "ClipboardHelper",
                Description = "A clipboard helper class.",
                CreateSample = new SampleFactory(() => new ClipboardHelperSample())
            },
            new SampleItem(){
                Title = "Dynamic Imaging",
                Description = "Dynamic Image Generation.",
                CreateSample = new SampleFactory(() => new ImagingSample())
            },
            new SampleItem(){
                Title = "IValueConverters",
                Description = "Generic IValueConverters for Data Binding.",
                CreateSample = new SampleFactory(() => new ConverterSamples())
            },
            new SampleItem(){
                Title = "Group Box",
                Description = "Group Box Control",
                CreateSample = new SampleFactory(() => new GroupBoxSample())
            },
            new SampleItem(){
                Title = "Data Services",
                Description = "ADO.NET Data Service Extension Methods",
                CreateSample = new SampleFactory(() => new DataServiceSample())
            }
        };

        public Page()
        {
            InitializeComponent();
            this.lstControls.ItemsSource = samples;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SampleItem item = (SampleItem)lstControls.SelectedItem;

            sampleTitle.Text = item.Title;
            sampleDescription.Text = item.Description;

            grdControlContent.Children.Clear();
            grdControlContent.Children.Add(item.CreateSample());
        }
    }
}
