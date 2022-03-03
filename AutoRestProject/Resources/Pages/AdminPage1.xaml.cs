using AutoRestProject.Classes.Models.BDModels;
using AutoRestProject.Classes.Models.ViewModels;
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
using System.Windows.Shapes;

namespace AutoRestProject.Resources.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage1.xaml
    /// </summary>
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

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // Открыть заказы
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // Открыть кухню
        {

        }
    }
}
