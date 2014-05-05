using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SilverlightContrib.Controls
{
    /// <summary>
    /// Represents a Group Box control which allows content to be grouped under a common header.
    /// </summary>
    public class GroupBox : ContentControl
    {
        // GroupBox control courtesy Tim Greenfield.
        // http://programmerpayback.com/2008/11/26/silverlight-groupbox-control/

        private RectangleGeometry FullRect;
        private RectangleGeometry HeaderRect;
        private ContentControl HeaderContainer;

        /// <summary>
        /// Creates a new instance of the GroupBox control.
        /// </summary>
        public GroupBox()
        {
            DefaultStyleKey = typeof(GroupBox);
            this.SizeChanged += GroupBox_SizeChanged;
        }

        /// <summary>
        /// Builds the visual tree for the GroupBox control when the template is applied. 
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            FullRect = (RectangleGeometry)GetTemplateChild("FullRect");
            HeaderRect = (RectangleGeometry)GetTemplateChild("HeaderRect");
            HeaderContainer = (ContentControl)GetTemplateChild("HeaderContainer");
            HeaderContainer.SizeChanged += HeaderContainer_SizeChanged;
        }

        /// <summary>
        /// Header Dependency Property
        /// </summary>
        public static readonly DependencyProperty HeaderProperty = 
            DependencyProperty.Register("Header", typeof(object), typeof(GroupBox), null);

        /// <summary>
        /// The Header for the group box.
        /// </summary>
        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>
        /// HeaderTemplate dependency property.
        /// </summary>
        public static readonly DependencyProperty HeaderTemplateProperty = 
            DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(GroupBox), null);

        /// <summary>
        /// The Header Data Template.
        /// </summary>
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }

        private void GroupBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FullRect.Rect = new Rect(new Point(), e.NewSize);
        }

        private void HeaderContainer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            HeaderRect.Rect = new Rect(new Point(HeaderContainer.Margin.Left, 0), e.NewSize);
        } 
    }
}
