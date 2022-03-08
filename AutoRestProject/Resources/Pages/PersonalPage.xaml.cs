using AutoRestProject.Classes.Models.BDModels;
using AutoRestProject.Classes.Models.ViewModels;
using AutoRestProject.Resources.UserControls;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для PersonalPage.xaml
    /// </summary>
    public partial class PersonalPage : Page
    {
        Personal Emp;
        PersonalPageViewModel ViewModel = new PersonalPageViewModel();
        public bool CanScroll { get; set; } = true;

        public PersonalPage(Personal Emp)
        {
            DataContext = ViewModel;
            this.Emp = Emp;

            
            UpdatePos();
            InitializeComponent();
            Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // назад
        {
            PageController.getInstance()?.Goto(new AdminPage1(Emp));
        }

        public void UpdatePos()
        {
            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                List<Position>? positions = bd.Positions?.ToList();
                if (positions == null) return;
                ViewModel.Positions = new ObservableCollection<Position>(positions);
                ViewModel.SelectedPos = positions[0];
            }
        }


        public void Update()
        {
            stack.Children.Clear();
            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                List<Personal>? personals = bd.Personals?.Include(u => u.Position).ToList();
                if (personals == null) return;
                foreach (Personal personal in personals)
                {
                    stack.Children.Add(new PersonalUserControl(this, personal));
                }
            }
        }



        Point scrollMousePoint = new Point();
        double hOff = 1;
        private void scrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            scrollMousePoint = e.GetPosition(scrollviewer);
            hOff = scrollviewer.VerticalOffset;
            if (CanScroll == true)
                scrollviewer.CaptureMouse();
        }
        private void scrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (scrollviewer.IsMouseCaptured)
                scrollviewer.ScrollToVerticalOffset(hOff + (scrollMousePoint.Y - e.GetPosition(scrollviewer).Y));
        }
        private void scrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollviewer.ReleaseMouseCapture();
        }

        private void scrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollviewer.ScrollToHorizontalOffset(scrollviewer.VerticalOffset + e.Delta);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // add
        {
            using(AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {

            }
        }
    }
}
