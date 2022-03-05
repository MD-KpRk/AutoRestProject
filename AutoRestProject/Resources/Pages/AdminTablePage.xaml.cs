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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoRestProject.Resources.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminTablePage.xaml
    /// </summary>
    public partial class AdminTablePage : Page
    {
        Personal Emp;
        AdminTablePageViewModel ViewModel = new AdminTablePageViewModel();

        public AdminTablePage(Personal Emp)
        {
            this.Emp = Emp;
            ViewModel.PersPos = Emp.Position.Title;
            ViewModel.PersName = Emp.First_name + " " + Emp.Second_name;
            ViewModel.Update();
            DataContext = ViewModel;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Назад
        {
            PageController.getInstance()?.Goto(new AdminPage1(Emp));
        }
    }
}
