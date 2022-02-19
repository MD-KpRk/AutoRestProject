using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AutoRestProject.Classes.Models.BDModels;
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
    /// Логика взаимодействия для WaiterPage1.xaml
    /// </summary>
    public partial class WaiterPage1 : Page
    {
        ViewModels.WaiterPage1ViewModel ViewModel = new();
        public WaiterPage1(Personal emp)
        {
            ViewModel.SetPerson(emp.first_name + " " + emp.second_name,emp.Position.Title);
            DataContext = ViewModel;
            InitializeComponent();
        }

        Point scrollMousePoint = new Point();
        double hOff = 1;
        private void scrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            scrollMousePoint = e.GetPosition(scrollviewer);
            hOff = scrollviewer.HorizontalOffset;
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
