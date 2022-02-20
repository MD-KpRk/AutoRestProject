using AutoRestProject.Classes.Models.BDModels;
using AutoRestProject.ViewModels;
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
    /// Логика взаимодействия для ChiefPage1.xaml
    /// </summary>
    public partial class ChiefPage1 : Page
    {
        ChiefPage1ViewModel ViewModel = new ChiefPage1ViewModel();
        public ChiefPage1(Personal personal)
        {
            DataContext = ViewModel;

            InitializeComponent();
        }

        private void dg2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
