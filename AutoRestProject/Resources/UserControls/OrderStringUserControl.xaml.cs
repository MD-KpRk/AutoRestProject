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
        OrderStringUserControlViewModel ViewModel;

        CookPage1 page;
        Order_string order_String;

        public OrderStringUserControl(CookPage1 page, Order_string order_String)
        {
            this.page = page;
            this.order_String = order_String;

            ViewModel = new OrderStringUserControlViewModel(order_String);

            ViewModel.OrderString = this.order_String.Food.Title;
            ViewModel.OrderNum = this.order_String.OrderId.ToString();
            if (this.order_String.CookPers == null)
            {
                ViewModel.OrderPers = "Не принят";
            }
            else
            {
                ViewModel.OrderPers = this.order_String.CookPers.First_name + " " + this.order_String.CookPers.Second_name;
            }
            DataContext = ViewModel;
            InitializeComponent();
        }

        public void UpdateTime()
        {
            ViewModel.UpdateTimer();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // открытие панели действий
        {
            page.ViewModel.ShowPanel();
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
