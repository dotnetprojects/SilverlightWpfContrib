using System;

namespace SilverlightContrib.Controls
{
    /// <summary>
    /// Delegate for the RatingChangedEvent.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void RatingChangedEventHandler(object sender, RatingChangedEventArgs e);

    /// <summary>
    /// Event data for the RatingChanged event.
    /// </summary>
    public class RatingChangedEventArgs : EventArgs
    {
        private readonly int _rating;

        /// <summary>
        /// Creates a new RatingChangedEventArgs instance.
        /// </summary>
        /// <param name="rating">The current rating.</param>
        public RatingChangedEventArgs(int rating)
        {
            _rating = rating;
        }

        /// <summary>
        /// The current rating of the control.
        /// </summary>
        public int Rating
        {
            get { return _rating; }
        }

    }
}
