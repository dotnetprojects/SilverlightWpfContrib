using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using SilverlightContrib.Xaml.Emf;

namespace SilverlightContrib.Controls
{
    /// <summary>
    /// Represents an Enhanced Metafile image.
    /// </summary>
    public class Emf : ContentControl
    {
        /// <summary>
        /// The Source dependency property.
        /// </summary>
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(EmfSource), typeof(Emf), new PropertyMetadata(OnSourcePropertyChanged));
        /// <summary>
        /// The Stretch dependency property.
        /// </summary>
        public static readonly DependencyProperty StretchProperty = DependencyProperty.Register("Stretch", typeof(Stretch), typeof(Emf), new PropertyMetadata(OnStretchPropertyChanged));
        /// <summary>
        /// Occurs while the download is in progress.
        /// </summary>
        public event EventHandler<DownloadProgressEventArgs> DownloadProgress;
        /// <summary>
        /// Occurs when there is an error associated with image retrieval or format.
        /// </summary>
        public event EventHandler<ExceptionEventArgs> ImageFailed;

        private double contentWidth = 0;
        private double contentHeight = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Emf"/> class.
        /// </summary>
        public Emf()
        {
            this.Stretch = Stretch.Uniform;
        }

        private static void OnStretchPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Emf).OnStretchPropertyChanged(e);
        }

        private static void OnSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Emf).OnSourcePropertyChanged(e);
        }

        private void OnStretchPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            InvalidateMeasure();
        }

        private void OnSourcePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            EmfSource newSource = (EmfSource)e.NewValue;
            if (newSource != null)
            {
                newSource.Owner = this;     // may raise exception

                newSource.DownloadProgress += source_DownloadProgress;
                newSource.ImageFailed += source_ImageFailed;
            }

            EmfSource oldSource = (EmfSource)e.OldValue;
            if (oldSource != null)
            {
                oldSource.DownloadProgress -= source_DownloadProgress;
                oldSource.ImageFailed -= source_ImageFailed;
            }
        }

        private void source_DownloadProgress(object sender, DownloadProgressEventArgs e)
        {
            if (this.DownloadProgress != null)
            {
                this.DownloadProgress(this, e);
            }
        }

        private void source_ImageFailed(object sender, ExceptionEventArgs e)
        {
            if (this.ImageFailed != null)
            {
                this.ImageFailed(this, e);
            }
        }

        private void Repaint(Size size)
        {
            if (size.Width == 0 || size.Height == 0 || this.contentWidth == 0 || this.contentHeight == 0 ||
                double.IsInfinity(size.Width) || double.IsInfinity(size.Height))
            {
                return;
            }
            TransformGroup group = new TransformGroup();
            ScaleTransform scale = new ScaleTransform();
            TranslateTransform translate = new TranslateTransform();

            if (this.Stretch != Stretch.None)
            {
                scale.ScaleX = size.Width / this.contentWidth;
                scale.ScaleY = size.Height / this.contentHeight;

                if (this.Stretch == Stretch.Uniform)
                {
                    if (scale.ScaleX < scale.ScaleY && size.Width > size.Height)
                    {
                        scale.ScaleY = scale.ScaleX;
                        translate.Y = (size.Height - this.contentHeight * scale.ScaleY) / 2;
                    }
                    else
                    {
                        scale.ScaleX = scale.ScaleY;
                        translate.X = (size.Width - this.contentWidth * scale.ScaleX) / 2;
                    }
                }
                else if (this.Stretch == Stretch.UniformToFill)
                {
                    if (scale.ScaleX > scale.ScaleY && size.Width > size.Height)
                    {
                        scale.ScaleY = scale.ScaleX;
                        translate.Y = (size.Height - this.contentHeight * scale.ScaleY) / 2;
                    }
                    else
                    {
                        scale.ScaleX = scale.ScaleY;
                        translate.X = (size.Width - this.contentWidth * scale.ScaleX) / 2;
                    }
                }
            }

            // render transform
            group.Children.Add(scale);
            group.Children.Add(translate);
            this.RenderTransform = group;

            // clip
            RectangleGeometry clip = new RectangleGeometry();
            clip.Rect = new Rect(-translate.X / scale.ScaleX, -translate.Y / scale.ScaleY, size.Width / scale.ScaleX, size.Height / scale.ScaleY);
            this.Clip = clip;
        }

        internal void SetResult(FrameworkElement element)
        {
            this.Content = element;
            this.contentWidth = 0;
            this.contentHeight = 0;

            if (element != null)
            {
                this.contentWidth = element.Width;
                this.contentHeight = element.Height;

                InvalidateMeasure();
            }
        }

        /// <summary>
        /// Gets or sets a value that describes how an image should be stretched to fill the destination rectangle. 
        /// </summary>
        /// <value>The stretch.</value>
        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        /// <summary>
        /// When implemented in a derived class, provides the behavior for the "Measure" pass of Silverlight layout.
        /// </summary>
        /// <param name="availableSize">The available size that this element can give to child elements. Infinity can be specified as a value to indicate that the element will size to whatever content is available.</param>
        /// <returns>
        /// The size that this element determines it needs during layout, based on its calculations of child element sizes.
        /// </returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            if (this.contentHeight == 0 || this.contentWidth == 0)
            {
                if (double.IsInfinity(availableSize.Height) || double.IsInfinity(availableSize.Width))
                {
                    return base.MeasureOverride(availableSize);
                }
                return availableSize;
            }

            double width = availableSize.Width;
            double height = availableSize.Height;
            double ratio = width / height;

            if (ratio == 0 || ratio == 1)
            {
                ratio = this.contentWidth / this.contentHeight;
            }

            if (double.IsInfinity(width) && double.IsInfinity(height))
            {
                width = this.contentWidth;
                height = this.contentHeight;
            }
            else if (!double.IsInfinity(height) && (double.IsInfinity(width) || ratio < 1))
            {
                width = (height / this.contentHeight) * this.contentWidth;
            }
            else
            {
                height = (width / this.contentWidth) * this.contentHeight;
            }

            return new Size(Math.Min(width, availableSize.Width), Math.Min(height, availableSize.Height));
        }

        /// <summary>
        /// When implemented in a derived class, provides the behavior for the "Arrange" pass of Silverlight layout.
        /// </summary>
        /// <param name="finalSize">The final area within the parent that this element should use to arrange itself and its children.</param>
        /// <returns>The actual size used.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            Repaint(finalSize);
            return finalSize;
        }

        /// <summary>
        /// Gets or sets the source for the EMF/WMF image.
        /// </summary>
        /// <value>The source.</value>
        [TypeConverter(typeof(EmfSourceConverter))]
        public EmfSource Source
        {
            get { return (EmfSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
    }
}
