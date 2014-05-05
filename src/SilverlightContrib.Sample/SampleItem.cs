using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightContrib.Sample
{
    /// <summary>
    /// The sample factory delegate
    /// </summary>
    public delegate FrameworkElement SampleFactory();

    /// <summary>
    /// Represents a sample item.
    /// </summary>
    public class SampleItem
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
        /// <summary>
        /// Creates the sample.
        /// </summary>
        /// <returns></returns>
        public SampleFactory CreateSample { get; set; }
    }
}
