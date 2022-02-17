using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AutoRestProject.Resources.Pages;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AutoRestProject
{


    public partial class MainWindow : Window
    {
        ViewModels.MainWindowViewModel ViewModel = new();

        public MainWindow()
        {
            DataContext = ViewModel;
            InitializeComponent();
            //ViewModel.ErrorBox.Show("awd");
            ErrorController.ErrorBox.Show("Hello Error");

            PageController.getInstance(MainFrame).Goto(new AuthPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e) 
        {
            ViewModel.ErrorBox.Hide();
        }
    }
}
