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
    /// Provides exception data for error events.
    /// </summary>
    public class ExceptionEventArgs
        : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionEventArgs"/> class.
        /// </summary>
        /// <param name="errorException">The error exception.</param>
        public ExceptionEventArgs(Exception errorException)
        {
            this.ErrorException = errorException;
        }

        /// <summary>
        /// Gets the underlying exception reported by the event.
        /// </summary>
        /// <value>The error exception.</value>
        public Exception ErrorException { get; private set; }
    }
}
