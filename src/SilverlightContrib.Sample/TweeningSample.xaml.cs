using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using SilverlightContrib.Tweener;

namespace SilverlightContrib.Sample
{
    public partial class TweeningSample : UserControl
    {
        public TweeningSample()
        {
            InitializeComponent();

            FillList(listRotate, Tween.GetTransitionType(animateRotate));
            FillList(listMove, Tween.GetTransitionType(animateMove));
        }

        private void FillList(ListBox list, TransitionType selection)
        {
            for (int i = 0; i < (int)TransitionType.EaseOutInBounce; i++) {
                list.Items.Add((TransitionType)i);

                if ((TransitionType)i == selection) {
                    list.SelectedIndex = i;
                }
            }
        }

        private void SetAnimation()
        {
            if (checkRotate == null || checkMove == null || listMove.SelectedIndex == -1 || listRotate.SelectedIndex == -1) {
                return;
            }

            storyboard.Stop();

            StringBuilder xaml = new StringBuilder();
            xaml.AppendLine("<Storyboard x:Name=\"storyboard\" AutoReverse=\"True\" RepeatBehavior=\"Forever\">");

            if (checkMove.IsChecked ?? false) {
                if (!storyboard.Children.Contains(animateMove)) {
                    storyboard.Children.Add(animateMove);
                }

                TransitionType type = (TransitionType)listMove.SelectedIndex;
                double fps = sliderMove.Value;

                Tween.SetTransitionType(animateMove, type);
                Tween.SetFps(animateMove, fps);
            }
            else {
                storyboard.Children.Remove(animateMove);
            }

            if (checkRotate.IsChecked ?? false) {
                if (!storyboard.Children.Contains(animateRotate)) {
                    storyboard.Children.Add(animateRotate);
                }

                TransitionType type = (TransitionType)listRotate.SelectedIndex;
                double fps = sliderRotate.Value;
                Tween.SetTransitionType(animateRotate, type);
                Tween.SetFps(animateRotate, fps);
            }
            else {
                storyboard.Children.Remove(animateRotate);
            }

            storyboard.Begin();
        }

        private void listRotate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetAnimation();
        }

        private void checkRotate_Checked(object sender, RoutedEventArgs e)
        {
            SetAnimation();
        }

        private void checkRotate_Unchecked(object sender, RoutedEventArgs e)
        {
            SetAnimation();
        }

        private void listMove_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetAnimation();
        }

        private void checkMove_Checked(object sender, RoutedEventArgs e)
        {
            SetAnimation();
        }

        private void checkMove_Unchecked(object sender, RoutedEventArgs e)
        {
            SetAnimation();
        }

        private void sliderMove_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetAnimation();
        }

        private void sliderRotate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetAnimation();
        }
    }
}
