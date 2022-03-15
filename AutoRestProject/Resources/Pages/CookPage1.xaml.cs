using AutoRestProject.Classes.Models.BDModels;
using AutoRestProject.Classes.Models.ViewModels;
using AutoRestProject.Resources.UserControls;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace AutoRestProject.Resources.Pages
{
    public partial class CookPage1 : Page
    {
        public bool CanScroll { get; set; } = true;
        public CookPage1ViewModel ViewModel;
        DispatcherTimer timer = new DispatcherTimer();

        Order_string? PanelOrder { get; set; }
        Personal Emp;


        public CookPage1(Personal pers)
        {
            ViewModel = new CookPage1ViewModel(this);
            Emp = pers;
            ViewModel.SetPerson(Emp);
            DataContext = ViewModel;

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();

            InitializeComponent();
            UpdateInfo();

        }

        void timer_Tick(object? sender, EventArgs e)
        {
            foreach (object? a in stack.Children)
            {
                (a as OrderStringUserControl)?.UpdateTime();
            }
        }

        public void UpdateInfo()
        {
            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                stack.Children.Clear();

                List<Order>? list = bd.Orders?
                    .Include(u => u.Table)
                    .Include(u => u.Personal)
                        .ThenInclude(u => u.Position)
                    .Include(u => u.Order_status)
                    .Include(u => u.Order_strings)
                        .ThenInclude(y => y.Food)
                    .ToList();
                _ = bd.Order_strings?
                    .Include(u => u.CookPers)
                    .Include(u => u.Food)
                    .Include(u => u.Order_string_status).ToList();

                if (list == null)
                {
                    return;
                }

                foreach (Order? o in list)
                {
                    foreach (Order_string? a in o.Order_strings)
                    {
                        if (a.Order_string_status.Title != ConfigController.getInstance().OrderStringDone)
                        {
                            if (!ViewModel.CB1 && !ViewModel.CB2 && !ViewModel.CB3) // Нет фильтра
                            {
                                stack.Children.Add(new OrderStringUserControl(this, a));
                            }
                            else if (ViewModel.CB1 && a.CookPersId == Emp.id) // Только мои блюда
                            {
                                stack.Children.Add(new OrderStringUserControl(this, a));
                            }
                            else if (ViewModel.CB2 && a.CookPers == null) // Только непринятые
                            {
                                stack.Children.Add(new OrderStringUserControl(this, a));
                            }
                            else if (ViewModel.CB3 && a.CookPers != null) // Только принятые
                            {
                                stack.Children.Add(new OrderStringUserControl(this, a));
                            }
                        }
                    }
                }
            }
        }

        // Scroll Panel
        Point scrollMousePoint = new Point();
        double hOff = 1;
        private void scrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            scrollMousePoint = e.GetPosition(scrollviewer);
            hOff = scrollviewer.VerticalOffset;
            if (CanScroll == true)
            {
                scrollviewer.CaptureMouse();
            }
        }
        private void scrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (scrollviewer.IsMouseCaptured)
            {
                scrollviewer.ScrollToVerticalOffset(hOff + (scrollMousePoint.Y - e.GetPosition(scrollviewer).Y));
            }
        }
        private void scrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollviewer.ReleaseMouseCapture();
        }

        private void scrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollviewer.ScrollToHorizontalOffset(scrollviewer.VerticalOffset + e.Delta);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.HidePanel();
        }

        public void ShowPanel(Order_string ord)
        {
            PanelOrder = ord;
            ViewModel.SelectedOrderString = ord.Order.Id.ToString();
            ViewModel.SelectedString = ord.Food_count + "x " + ord.Food.Title;

            if (ord.CookPers == null)
            {
                ViewModel.SelectedPersName = "Никто не готовит";
            }
            else
            {
                ViewModel.SelectedPersName = ord.CookPers.First_name + "\n" + ord.CookPers.Second_name;
            }

            ViewModel.ShowPanel();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // Приготовлено
        {
            if (PanelOrder == null)
            {
                return;
            }

            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                Order_string? os = bd.Order_strings?.Where(u => u.ID == PanelOrder.ID).Include(u => u.CookPers).FirstOrDefault();
                if (os == null)
                {
                    return;
                }

                if (os.CookPers == null)
                {
                    os.CookPersId = Emp.id;
                    bd.SaveChanges();
                }

                Order_string_status? ordstat = bd.Order_string_statuses?.Where(u => u.Title == ConfigController.getInstance().OrderStringDone).FirstOrDefault();

                if (ordstat == null)
                {
                    ErrorBox.getInstance().Show("Ошибка связи базы данных и программы");
                    return;
                }
                os.Order_string_status = ordstat;
                bd.SaveChanges();
                ViewModel.HidePanel();
                UpdateInfo();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // Принять блюдо
        {
            if (PanelOrder == null)
            {
                return;
            }

            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                Order_string? os = bd.Order_strings?.Where(u => u.ID == PanelOrder.ID).FirstOrDefault();
                if (os == null)
                {
                    return;
                }

                Order_string_status? ordstat = bd.Order_string_statuses?.Where(u => u.Title == ConfigController.getInstance().OrderStringProcessing).FirstOrDefault();

                if (ordstat == null)
                {
                    ErrorBox.getInstance().Show("Ошибка связи базы данных и программы");
                    return;
                }

                os.Order_string_status = ordstat;
                os.CookPers = Emp;
                bd.SaveChanges();
                ViewModel.HidePanel();
                UpdateInfo();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) // Отменить готовку
        {
            if (PanelOrder == null)
            {
                return;
            }

            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                Order_string? os = bd.Order_strings?.Where(u => u.ID == PanelOrder.ID).FirstOrDefault();
                if (os == null)
                {
                    return;
                }

                Order_string_status? ordstat = bd.Order_string_statuses?.Where(u => u.Title == ConfigController.getInstance().OrderStringNotDone).FirstOrDefault();

                if (ordstat == null)
                {
                    ErrorBox.getInstance().Show("Ошибка связи базы данных и программы");
                    return;
                }

                os.Order_string_status = ordstat;
                os.CookPers = null;
                os.CookPersId = null;

                bd.SaveChanges();
                ViewModel.HidePanel();
                UpdateInfo();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            PageController.getInstance()?.Goto(new AuthPage());
        }
    }
}
