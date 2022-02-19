using AutoRestProject.Classes.Models.BDModels;
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
    /// Логика взаимодействия для OrderUserControl.xaml
    /// </summary>
    public partial class OrderUserControl : UserControl
    {
        ViewModels.OrderUserControlViewModel ViewModel = new();
        public OrderUserControl(WaiterPage1 page, Order order)
        {
            ViewModel.OrderNum = order.Id;
            DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
