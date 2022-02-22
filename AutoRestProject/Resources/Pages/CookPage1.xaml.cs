using AutoRestProject.Resources.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoRestProject.Resources.Pages
{
    /// <summary>
    /// Логика взаимодействия для CookPage1.xaml
    /// </summary>
    public partial class CookPage1 : Page
    {
        public bool CanScroll { get; set; } = true;

        public CookPage1()
        {
            InitializeComponent();

            OrderStringUserControl[] os = new OrderStringUserControl[10];

            for(int i =0;i<os.Length;i++)
            {
                os[i] = new OrderStringUserControl();
                stack.Children.Add(new OrderStringUserControl());

            }

        }




        // Scroll Panel
        Point scrollMousePoint = new Point();
        double hOff = 1;
        private void scrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            scrollMousePoint = e.GetPosition(scrollviewer);
            hOff = scrollviewer.HorizontalOffset;
            if (CanScroll == true)
                scrollviewer.CaptureMouse();
        }
        private void scrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (scrollviewer.IsMouseCaptured)
                scrollviewer.ScrollToHorizontalOffset(hOff + (scrollMousePoint.X - e.GetPosition(scrollviewer).X));
        }
        private void scrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollviewer.ReleaseMouseCapture();
        }

        private void scrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollviewer.ScrollToHorizontalOffset(scrollviewer.HorizontalOffset + e.Delta);
        }
    }
}
