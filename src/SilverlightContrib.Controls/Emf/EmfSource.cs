using System;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using SilverlightContrib.Xaml.Emf;

namespace SilverlightContrib.Controls
{
    /// <summary>
    /// Represents the EMF source.
    /// </summary>
    public class EmfSource
        : BaseSource
    {
        /// <summary>
        /// Occurs when there is an error associated with image retrieval or format.
        /// </summary>
        public event EventHandler<ExceptionEventArgs> ImageFailed;

        private Emf owner;
        private FrameworkElement result;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmfSource"/> class.
        /// </summary>
        public EmfSource()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmfSource"/> class.
        /// </summary>
        /// <param name="uriSource">The URI source.</param>
        public EmfSource(Uri uriSource)
            : base(uriSource)
        {
        }

        internal Emf Owner
        {
            set
            {
                if (this.owner != null && value != null) {
                    throw new ArgumentException(SilverlightContrib.Resources.ExceptionEmfSourceReuse);
                }
                this.owner = value;

                if (this.owner != null) {
                    this.owner.SetResult(this.result);
                }
            }
        }

        private void OnImageFailed(Exception exception)
        {
            if (this.ImageFailed != null) {
                this.ImageFailed(this, new ExceptionEventArgs(exception));
            }
        }

        /// <summary>
        /// Called when the download has failed.
        /// </summary>
        /// <param name="exception">The exception.</param>
        protected override void OnDownloadFailed(Exception exception)
        {
            OnImageFailed(exception);
        }

        /// <summary>
        /// Called when the source stream is available.
        /// </summary>
        /// <param name="stream"></param>
        protected override void OnStreamAvailable(Stream stream)
        {
            try {
                EmfConverter converter = new EmfConverter();
                converter.TreatWarningAsError = false;

                string xaml = converter.ToXaml(stream);
                this.result = (FrameworkElement)XamlReader.Load(xaml);

                if (this.owner != null) {
                    this.owner.SetResult(this.result);
                }
            }
            catch (Exception e) {
                OnImageFailed(e);
            }
        }
    }
}
