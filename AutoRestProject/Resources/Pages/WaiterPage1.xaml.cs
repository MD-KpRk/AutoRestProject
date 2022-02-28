﻿using System;
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
using Microsoft.EntityFrameworkCore;

namespace AutoRestProject.Resources.Pages
{
    /// <summary>
    /// Логика взаимодействия для WaiterPage1.xaml
    /// </summary>
    public partial class WaiterPage1 : Page
    {
        ViewModels.WaiterPage1ViewModel ViewModel = new();
        public bool CanScroll { get; set; } = true;

        Personal CurrentUser;

        public WaiterPage1(Personal emp)
        {
            CurrentUser = emp;
            ViewModel.SetPerson(emp.First_name + " " + emp.Second_name,emp.Position.Title);
            DataContext = ViewModel;

            InitializeComponent();

            List<Order>? list = new List<Order>();

            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                list = bd.Orders?
                    .Include(u => u.Table)
                    .Include(u => u.Personal)
                        .ThenInclude(u => u.Position)
                    .Include(u => u.Order_status)
                    .Include(u => u.Order_strings)
                        .ThenInclude(u => u.Food)
                    .ToList();
            }

            //UserControls.OrderUserControl[] d = new UserControls.OrderUserControl[10];

            for(int i=0;i<list?.Count();i++)
            {
                stack.Children.Add(new UserControls.OrderUserControl(this,list[i]));
            }

            //List <UserControls.OrderUserControl> = new List<UserControls.OrderUserControl>();


            //stack.Children.Add(d); 


        }


        // Scroll Panel
        Point scrollMousePoint = new Point();
        double hOff = 1;
        private void scrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            scrollMousePoint = e.GetPosition(scrollviewer);
            hOff = scrollviewer.HorizontalOffset;
            if(CanScroll == true)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PanelClose();
        }

        public void ShowPanel(Order order)
        {
            ViewModel.OrderNum = order.Id;
            ViewModel.OrderPersName = order.Personal.First_name + " " + order.Personal.Second_name;
            ViewModel.OrderPersPos = order.Personal.Position.Title;
            ViewModel.PanelShow();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // + заказ
        {
            PageController.getInstance()?.Goto(new AddOrderPage1(CurrentUser));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // Выход
        {
            PageController.getInstance()?.Goto(new AuthPage());
        }
    }
}
