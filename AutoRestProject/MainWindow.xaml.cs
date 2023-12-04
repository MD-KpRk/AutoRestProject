using AutoRestProject.Resources.Pages;
using Microsoft.Data.SqlClient;
using System;
using System.Linq;
using System.Windows;

namespace AutoRestProject
{
    public partial class MainWindow : Window
    {
        ViewModels.MainWindowViewModel ViewModel = new();

        public MainWindow()
        {
            DataContext = ViewModel;
            try
            {
                InitializeBD();
            }
            catch (SqlException)
            {
                InitializeComponent();
                ViewModel.ErrorBox.Show("Ошибка соединения с базой данных");
                return;
            }

            InitializeComponent();
            PageController.getInstance(MainFrame).Goto(new AuthPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ErrorBox.Hide();
        }

        void InitializeBD()
        {
            using (AutoRestBDContext bd = new(ConfigController.getInstance().ConOptions))
            {
                if (bd.Personals == null)
                {
                    // создать бд ???
                    return;
                }

                bd.Personals.FirstOrDefault();
            }
        }
    }
}
