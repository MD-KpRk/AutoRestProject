﻿using AutoRestProject.Classes.Models.BDModels;
using AutoRestProject.Classes.Models.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace AutoRestProject.Resources.Pages
{
    public partial class AdminPage1 : Page
    {
        AdminPage1ViewModel ViewModel = new AdminPage1ViewModel();
        Personal Emp;

        public AdminPage1(Personal Emp)
        {
            this.Emp = Emp;
            ViewModel.PersPos = Emp.Position.Title;
            ViewModel.PersName = Emp.First_name + " " + Emp.Second_name;

            DataContext = ViewModel;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Выход
        {
            PageController.getInstance()?.Goto(new AuthPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // Открыть заказы
        {
            PageController.getInstance()?.Goto(new WaiterPage1(Emp));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // Открыть кухню
        {
            PageController.getInstance()?.Goto(new CookPage1(Emp));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) // Столики
        {
            PageController.getInstance()?.Goto(new AdminTablePage(Emp));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) // Управление персоналом
        {
            PageController.getInstance()?.Goto(new PersonalPage(Emp));
        }

        private void Button_Click_5(object sender, RoutedEventArgs e) // Статистика
        {
            PageController.getInstance()?.Goto(new StatPage(Emp));
        }
    }
}
