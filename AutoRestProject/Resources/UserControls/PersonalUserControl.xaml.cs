using AutoRestProject.Classes.Models.BDModels;
using AutoRestProject.Classes.Models.ViewModels;
using AutoRestProject.Resources.Pages;
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

namespace AutoRestProject.Resources.UserControls
{
    /// <summary>
    /// Логика взаимодействия для PersonalUserControl.xaml
    /// </summary>
    public partial class PersonalUserControl : UserControl
    {

        Personal Emp;
        PersonalPage Page;

        PersonalUserControlViewModel ViewModel = new PersonalUserControlViewModel();

        public PersonalUserControl(PersonalPage page, Personal emp)
        {
            ViewModel.PersPos = emp.Position.Title;
            ViewModel.PersPIN = emp.Pin.ToString();
            ViewModel.PersTel = emp.Phone_number;
            ViewModel.PersName = emp.First_name + " " + emp.Second_name + " " + emp.Patronymic;
            DataContext = ViewModel;
            Emp = emp;
            Page = page;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //page.ShowPanel(Order);

        }


        private void Button_MouseDown(object sender, RoutedEventArgs e)
        {
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            Page.CanScroll = false;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            Page.CanScroll = true;
        }

    }
}
