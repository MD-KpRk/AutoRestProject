using AutoRestProject.Classes.Models.BDModels;
using AutoRestProject.ViewModels;
using Microsoft.EntityFrameworkCore;
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

namespace AutoRestProject.Resources.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddOrderPage1.xaml
    /// </summary>
    public partial class AddOrderPage1 : Page
    {
        AddOrderPage1ViewModel ViewModel = new AddOrderPage1ViewModel();

        public AddOrderPage1(Personal? emp)
        {
            ViewModel.CurrPersName = emp?.First_name + " " + emp?.Second_name;
            ViewModel.CurrPersPos = emp?.Position?.Title + "";
            UpdateTables(1);
            DataContext = ViewModel;
            InitializeComponent();
        }

        void UpdateTables(int Seats)
        {
            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                List<Classes.Models.BDModels.Table>? list = bd.Tables?.Include(u => u.Table_Status)?.ToList();
                List<Classes.Models.BDModels.Table>? list2 = bd.Tables?.Include(u => u.Table_Status)?.Where(u => u.Seats >= Seats).ToList();
                if (list == null || list2 == null) return;

                ViewModel.Tables = new ObservableCollection<Classes.Models.BDModels.Table>(list);
                ViewModel.SortedTables = new ObservableCollection<Classes.Models.BDModels.Table>(list2);

            }
        }


        private void Button_Click(object sender, RoutedEventArgs e) // Назад
        {
            PageController.getInstance()?.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string? tag = (sender as Button)?.Tag as string;
            if(tag == null) return;
            if(tag == "-")
            {
                ViewModel.Sub();
                UpdateTables(ViewModel.Seats);
            }
            if(tag == "+")
            {
                ViewModel.Add();
                UpdateTables(ViewModel.Seats);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Classes.Models.BDModels.Table? table = dg1.SelectedItem as Classes.Models.BDModels.Table;
            if(table == null)
            {
                ErrorBox.getInstance().Show("Стол не выбран");
                return;
            }    
            Continue(table);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Classes.Models.BDModels.Table? table = dg2.SelectedItem as Classes.Models.BDModels.Table;
            if (table == null)
            {
                ErrorBox.getInstance().Show("Стол не выбран");
                return;
            }
            Continue(table);
        }

        void Continue(Classes.Models.BDModels.Table table)
        {
            PageController.getInstance()?.Goto(new AddOrderPage2());
        }



    }
}
