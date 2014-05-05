using System;

namespace SilverlightContrib.Imaging
{
    /// <summary>
    /// Event data for the ImageError event.
    /// </summary>
    public class EditableImageErrorEventArgs : EventArgs
    {
        /// Original Editable Image class courtesy Joe Stegman.
        /// http://blogs.msdn.com/jstegman
        
        private string _errorMessage = string.Empty;

        /// <summary>
        /// The error message indicating the error condition experienced by EditableImage.
        /// </summary>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }
    }
}
