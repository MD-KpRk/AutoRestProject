﻿using AutoRestProject.Classes.Models.BDModels;
using AutoRestProject.Classes.Models.ViewModels;
using AutoRestProject.Resources.UserControls;
using Microsoft.EntityFrameworkCore;
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
using System.Windows.Threading;

namespace AutoRestProject.Resources.Pages
{
    /// <summary>
    /// Логика взаимодействия для CookPage1.xaml
    /// </summary>
    public partial class CookPage1 : Page
    {
        public bool CanScroll { get; set; } = true;
        public CookPage1ViewModel ViewModel = new CookPage1ViewModel();
        DispatcherTimer timer = new DispatcherTimer();

        Order_string? PanelOrder { get; set; }
        Personal Emp;


        public CookPage1(Personal pers)
        {
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
                (a as OrderStringUserControl)?.UpdateTime();
        }


        void UpdateInfo()
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


                if (list == null) return;


                foreach (var o in list)
                {
                    foreach (var a in o.Order_strings)
                    {
                        if(a.Order_string_status.Title != ConfigController.getInstance().OrderStringDone)
                            stack.Children.Add(new OrderStringUserControl(this, a));
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
                ViewModel.SelectedPersName = "Никто не готовит";
            else
                ViewModel.SelectedPersName = ord.CookPers.First_name + "\n" + ord.CookPers.Second_name;

            ViewModel.ShowPanel();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // Приготовлено
        {
            if (PanelOrder == null) return;

            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                Order_string? os = bd.Order_strings?.Where(u => u.ID == PanelOrder.ID).FirstOrDefault();
                if (os == null) return;
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
            if (PanelOrder == null) return;

            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                Order_string? os = bd.Order_strings?.Where(u => u.ID == PanelOrder.ID).FirstOrDefault();
                if (os == null) return;
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

        private void Button_Click_3(object sender, RoutedEventArgs e) // Отменить отправку
        {
            if (PanelOrder == null) return;

            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                Order_string? os = bd.Order_strings?.Where(u => u.ID == PanelOrder.ID).FirstOrDefault();
                if (os == null) return;
                Order_string_status? ordstat = bd.Order_string_statuses?.Where(u => u.Title == ConfigController.getInstance().OrderStringNotDone).FirstOrDefault();

                if (ordstat == null)
                {
                    ErrorBox.getInstance().Show("Ошибка связи базы данных и программы");
                    return;
                }

                os.Order_string_status = ordstat;
                os.CookPers = null;

                bd.SaveChanges();
                ViewModel.HidePanel();
                UpdateInfo();
            }
        }
    }
}
