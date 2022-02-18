using System.Linq;
using System.Windows;
using AutoRestProject.Resources.Pages;

namespace AutoRestProject
{
    public partial class MainWindow : Window
    {
        ViewModels.MainWindowViewModel ViewModel = new();

        public MainWindow()
        {
            DataContext = ViewModel;
            InitializeBD();
            InitializeComponent();
            ErrorBox.getInstance().Show("Hello Error");

            PageController.getInstance(MainFrame).Goto(new AuthPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e) 
        {
            ViewModel.ErrorBox.Hide();
        }

        void InitializeBD()
        {
            using(AutoRestBDContext bd = new(ConfigController.getInstance().ConOptions))
            {
                if (bd.Personals == null) return;
                bd.Personals.FirstOrDefault();
            }
        }


    }
}
