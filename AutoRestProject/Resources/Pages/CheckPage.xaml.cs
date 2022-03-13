using AutoRestProject.Classes.Models.BDModels;
using AutoRestProject.Classes.Models.ViewModels;
using System.Drawing.Printing;
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
    class Goods
    {
        public string name;
        public int code;
        public double price;
        public Goods(string name, int code, double price)
        {
            this.name = name;
            this.code = code;
            this.price = price;
        }
    }

    public partial class CheckPage : Page
    {
        Personal Emp;
        Order Order;
        CheckPageViewModel ViewModel = new CheckPageViewModel();
        public CheckPage(Personal emp, Order order)
        {
            Order = order;
            Emp = emp;
            ViewModel.Update(Order);
            DataContext = ViewModel;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PageController.getInstance()?.Goto(new WaiterPage1(Emp));
        }


        
    }
}
