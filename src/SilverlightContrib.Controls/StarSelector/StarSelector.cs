using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;

namespace SilverlightContrib.Controls
{
    /// <summary>
    /// Represents a Star Selector for which a user can select a rating.
    /// </summary>
    [TemplatePart(Name="RootElement", Type=typeof(Panel))]
    public class StarSelector : Control
    {
        private const int STARSELECTOR_StarCount = 4;

        private readonly List<Star> _stars;
        private int _rating;

        /// <summary>
        /// Fired when the rating is changed.
        /// </summary>
        public event RatingChangedEventHandler RatingChanged;

        /// <summary>
        /// Creates an instance of the StarSelector Control.
        /// </summary>
        public StarSelector()
        {
            DefaultStyleKey = typeof(StarSelector);
            _stars = new List<Star>();

            this.MouseEnter += new MouseEventHandler(StarContainer_MouseEnter);
            this.MouseLeave += new MouseEventHandler(StarContainer_MouseLeave);
        }

        /// <summary>
        /// Event that fires when the rating changes on the StarSelector control.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRatingChanged(RatingChangedEventArgs e)
        {
            if (RatingChanged != null)
            {
                RatingChanged(this, e);
            }
        }

        private void StarContainer_MouseLeave(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Container Mouse Leave");
            UpdateControlVisualState();
        }

        private void UpdateControlVisualState()
        {
            for (int i = 0; i < _stars.Count; ++i)
            {
                _stars[i].SuspendVisualChanges = true;
                try
                {
                    _stars[i].Highlighted = false;
                    _stars[i].RatingMode = true;
                    _stars[i].RatingSelected = i <= (_rating - 1);
                }
                finally
                {
                    _stars[i].SuspendVisualChanges = false;
                    _stars[i].UpdateVisualState();
                }
            }
        }

        private void StarContainer_MouseEnter(object sender, MouseEventArgs e)
        {
            if (ReadOnly)
            {
                return;
            }

            // System.Diagnostics.Debug.WriteLine("Container Mouse Enter");
            for (int i = 0; i < _stars.Count; ++i)
            {
                _stars[i].SuspendVisualChanges = true;
                try
                {
                    _stars[i].RatingMode = false;
                    _stars[i].RatingSelected = false;
                }
                finally
                {
                    _stars[i].SuspendVisualChanges = false;
                    _stars[i].UpdateVisualState();
                }
            }
        }

        /// <summary>
        /// Builds the visual tree for the StarSelector control when the template is applied. 
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            RootElement = GetTemplateChild("RootElement") as Panel;
            for (int i = 0; i < STARSELECTOR_StarCount; ++i)
            {
                Star ratingStar = new Star();
                ratingStar.Template = RootElement.Resources["StarTemplate"] as ControlTemplate;
                ratingStar.RatingMode = true;
                ratingStar.StarIndex = i;
                ratingStar.RatingSelected = i <= (_rating - 1);

                ratingStar.MouseEnter += new MouseEventHandler(ratingStar_MouseEnter);
                ratingStar.MouseLeftButtonUp += new MouseButtonEventHandler(ratingStar_MouseLeftButtonUp);

                RootElement.Children.Add(ratingStar);

                _stars.Add(ratingStar);
            }
        }

        private void ratingStar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Don't respond to mouse events if we are ReadOnly
            if (ReadOnly)
            {
                return;
            }

            Star currStar = sender as Star;
            _rating = currStar.StarIndex + 1;
            OnRatingChanged(new RatingChangedEventArgs(currStar.StarIndex + 1));
        }

        private void ratingStar_MouseEnter(object sender, MouseEventArgs e)
        {
            // Don't respond to mouse events if we are ReadOnly
            if (ReadOnly)
            {
                return;
            }

            Star currStar = sender as Star;
            for (int i = 0; i < _stars.Count; ++i)
            {
                _stars[i].Highlighted = i <= currStar.StarIndex;
                _stars[i].UpdateVisualState();
            }
        }

        private Panel RootElement
        {
            get;
            set;
        }

        #region Rating Dependency Property

        /// <summary>
        /// Gets or sets the selected rating for the control.
        /// </summary>
        public int SelectedRating
        {
            get { return (int)GetValue(SelectedRatingProperty); }
            set { SetValue(SelectedRatingProperty, value); }
        }

        /// <summary>
        /// SelectedRating Dependency Property.
        /// </summary>
        public static readonly DependencyProperty SelectedRatingProperty =
            DependencyProperty.Register(
            "SelectedRating",
            typeof(int),
            typeof(StarSelector),
            new PropertyMetadata(new PropertyChangedCallback(SelectedRatingChanged)));

        private static void SelectedRatingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StarSelector s = d as StarSelector;

            if (s != null)
            {
                s._rating = (int)e.NewValue;
                s.UpdateControlVisualState();
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="StarSelector"/> is in a read only state.
        /// </summary>
        /// <value><c>true</c> if read only; otherwise, <c>false</c>.</value>
        public bool ReadOnly
        {
            get { return (bool)GetValue(ReadOnlyProperty); }
            set { SetValue(ReadOnlyProperty, value); }
        }

        /// <summary>
        /// Determines whether the <see cref="StarSelector"/> is in a read only state.
        /// </summary>
        public static readonly DependencyProperty ReadOnlyProperty =
            DependencyProperty.Register(
                "ReadOnly",
                typeof(bool),
                typeof(StarSelector),
                new PropertyMetadata(new PropertyChangedCallback(ReadOnlyChanged)));

        /// <summary>
        /// Invoked when the <see cref="ReadOnlyProperty"/> is changed.
        /// </summary>
        /// <param name="d">The <see cref="DependencyObject"/> that raised the event.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void ReadOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StarSelector s = d as StarSelector;

            if (s != null)
            {
                s.ReadOnly = (bool)e.NewValue;
                s.UpdateControlVisualState();
            }
        }
    }
}
