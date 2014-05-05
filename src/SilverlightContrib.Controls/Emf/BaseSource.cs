using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Resources;

namespace SilverlightContrib.Controls
{
    /// <summary>
    /// Represents a generic source object.
    /// </summary>
    public abstract class BaseSource : DependencyObject
    {
        /// <summary>
        /// The DownloadProgress dependency property.
        /// </summary>
        public static readonly DependencyProperty DownloadProgressProperty = DependencyProperty.Register("DownloadProgress", typeof(int), typeof(BaseSource), null);
        /// <summary>
        /// The UriSource dependency property.
        /// </summary>
        public static readonly DependencyProperty UriSourceProperty = DependencyProperty.Register("UriSource", typeof(Uri), typeof(BaseSource), new PropertyMetadata(OnUriSourcePropertyChanged));

        /// <summary>
        /// Occurs while the download is in progress.
        /// </summary>
        public event EventHandler<DownloadProgressEventArgs> DownloadProgress;

        private WebClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSource"/> class.
        /// </summary>
        protected BaseSource()
        {
            this.client = new WebClient();
            this.client.DownloadProgressChanged += client_DownloadProgressChanged;
            this.client.OpenReadCompleted += client_OpenReadCompleted;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSource"/> class.
        /// </summary>
        /// <param name="uriSource">The URI source.</param>
        protected BaseSource(Uri uriSource)
            : this()
        {
            this.UriSource = uriSource;
        }

        private static void OnUriSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as BaseSource).OnUriSourcePropertyChanged(e);
        }

        private void OnUriSourcePropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (this.client.IsBusy){
                this.client.CancelAsync();
            }

            Uri source = (Uri)e.NewValue;
            if (source != null) {
                try {
                    StreamResourceInfo resource = null;

                    if (!source.IsAbsoluteUri) {
                        // first try embedded resource
                        resource = Application.GetResourceStream(source);
                    }
                    if (resource != null) {
                        OnDownloadProgress(new DownloadProgressEventArgs(100));

                        using (resource.Stream) {
                            OnStreamAvailable(resource.Stream);
                        }
                    }
                    else {
                        // try the web
                        this.client.OpenReadAsync(source);
                    }
                }
                catch (Exception ex) {
                    OnDownloadFailed(ex);
                }
            }
        }

        /// <summary>
        /// Gets or sets the uri source.
        /// </summary>
        /// <value>The URI source.</value>
        public Uri UriSource
        {
            get { return (Uri)GetValue(UriSourceProperty); }
            set { SetValue(UriSourceProperty, value); }
        }

        /// <summary>
        /// Sets the stream source.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public void SetSource(Stream stream)
        {
            this.UriSource = null;

            OnStreamAvailable(stream);
        }

        /// <summary>
        /// Called when the download has failed.
        /// </summary>
        /// <param name="exception">The exception.</param>
        protected abstract void OnDownloadFailed(Exception exception);

        /// <summary>
        /// Called when the stream is available.
        /// </summary>
        /// <param name="stream">The stream.</param>
        protected abstract void OnStreamAvailable(Stream stream);

        private void OnDownloadProgress(DownloadProgressEventArgs e)
        {
            if (this.DownloadProgress != null) {
                this.DownloadProgress(this, e);
            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            OnDownloadProgress(new DownloadProgressEventArgs(e.ProgressPercentage));
        }

        private void client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null) {
                OnDownloadFailed(e.Error);
            }
            else if (!e.Cancelled && e.Result != null) {
                using (e.Result) {
                    OnStreamAvailable(e.Result);
                }
            }
        }
    }
}
