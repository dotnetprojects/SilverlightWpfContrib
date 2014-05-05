using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightContrib.Controls
{
    /// <summary>
    /// Provides progress data for download events.
    /// </summary>
    public class DownloadProgressEventArgs
        : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadProgressEventArgs"/> class.
        /// </summary>
        /// <param name="progress">The progress.</param>
        public DownloadProgressEventArgs(int progress)
        {
            this.Progress = progress;
        }

        /// <summary>
        /// Gets the progress reported by the event.
        /// </summary>
        /// <value>The progress.</value>
        public int Progress { get; private set; }
    }
}
