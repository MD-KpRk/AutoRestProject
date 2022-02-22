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
    /// Логика взаимодействия для OrderStringUserControl.xaml
    /// </summary>
    public partial class OrderStringUserControl : UserControl
    {
        OrderStringUserControlViewModel ViewModel = new OrderStringUserControlViewModel();

        CookPage1 page;
        Order_string order_String;

        public OrderStringUserControl(CookPage1 page, Order_string order_String)
        {
            this.page = page;
            this.order_String = order_String;
            DataContext = ViewModel;
            ViewModel.OrderString = order_String.Food.Title;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            page.CanScroll = false;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            page.CanScroll = true;
        }
    }
}
