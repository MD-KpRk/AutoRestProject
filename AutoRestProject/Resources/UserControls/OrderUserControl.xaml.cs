using AutoRestProject.Classes.Models.BDModels;
using AutoRestProject.Resources.Pages;
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

namespace AutoRestProject.Resources.UserControls
{
    public partial class OrderUserControl : UserControl
    {
        ViewModels.OrderUserControlViewModel ViewModel = new();

        WaiterPage1 page;
        Order Order;

        public OrderUserControl(WaiterPage1 page, Order order)
        {
            Order = order;
            ViewModel.OrderNum = order.Id;
            ViewModel.OrderStatus = order.Order_status.Title;
            ViewModel.TableNum = order.Table.Id;
            this.page = page;
            ViewModel.EmpName = order.Personal.Second_name + " " + order.Personal.First_name;
            ViewModel.Order_Strings = new ObservableCollection<Order_string>(order.Order_strings);
            DataContext = ViewModel;
            InitializeComponent();
        }


        private void Button_MouseDown(object sender, RoutedEventArgs e)
        {
            page.ShowPanel(Order);
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
