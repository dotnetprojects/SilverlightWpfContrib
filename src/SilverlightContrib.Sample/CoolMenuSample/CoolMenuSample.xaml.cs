using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using SilverlightContrib.Controls;

namespace SilverlightContrib.Sample
{
    public partial class CoolMenuSample : UserControl
    {
        private readonly Random m_random;

        public CoolMenuSample()
        {
            InitializeComponent();
            this.Loaded += CoolMenuSample_Loaded;
            m_random = new Random();
        }

        void CoolMenuSample_Loaded(object sender, RoutedEventArgs e)
        {
            var col = new ObservableCollection<string>
                {
                    "/Images/CoolMenuImages/Icons/box.png",
                    "/Images/CoolMenuImages/Icons/bomb.png",
                    "/Images/CoolMenuImages/Icons/calc.png",
                    "/Images/CoolMenuImages/Icons/fish.png",
                    "/Images/CoolMenuImages/Icons/star.png"
                };
            CoolMenuBinding.ItemsSource = col;

            for(int i=0; i<6; ++i)
                AddRectangle();
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddRectangle();
        }

        private void AddRectangle()
        {
            coolMenuRectangle.Items.Add(new Rectangle
                                            {
                                                Margin = new Thickness(5),
                                                StrokeThickness = 10,
                                                Stroke = new SolidColorBrush(GetRandomColor())
                                            });
        }

        private Color GetRandomColor()
        {
            byte[] colorBytes = new byte[3];
            m_random.NextBytes(colorBytes);
            return Color.FromArgb(255, colorBytes[0], colorBytes[1], colorBytes[2]);
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (coolMenuRectangle.Items.Count > 0)
                coolMenuRectangle.Items.RemoveAt(coolMenuRectangle.Items.Count - 1);
        }
    }

    public class CustomCoolMenuBehavior : DefaultCoolMenuBehavior
    {
        public override void Initialize(CoolMenu parent, CoolMenuItem element)
        {
            base.Initialize(parent, element);
            element.Background = new SolidColorBrush(Colors.Yellow);
        }
    }
}
