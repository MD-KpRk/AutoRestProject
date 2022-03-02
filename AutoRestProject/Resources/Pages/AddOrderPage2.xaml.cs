using AutoRestProject.Classes.Models.BDModels;
using AutoRestProject.Classes.Models.ViewModels;
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
    /// Логика взаимодействия для AddOrderPage2.xaml
    /// </summary>
    public partial class AddOrderPage2 : Page
    {
        AddOrderPage2ViewModel ViewModel = new AddOrderPage2ViewModel();

        List<Menu_string> Order_Strings = new List<Menu_string>();

        Classes.Models.BDModels.Table? table;

        Personal? Curr_Emp;

        public AddOrderPage2(Classes.Models.BDModels.Table table, Personal personal)
        {
            ViewModel.CurrPersName = personal?.First_name + " " + personal?.Second_name;
            ViewModel.CurrPersPos = personal?.Position?.Title + "";
            this.table = table;
            Curr_Emp = personal;
            UpdateMenu();
            DataContext = ViewModel;
            InitializeComponent();

        }


        public void UpdateMenu()
        {
            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                List<Menu_string>? list = bd.Menu_strings?.Include( u => u.Food).ToList();
                if (list == null) return; 

                ViewModel.Menu = new ObservableCollection<Menu_string>(list);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Back
        {
            PageController.getInstance()?.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // add orderstring
        {
            Menu_string? menu_String = dg1.SelectedItem as Menu_string;
            if (menu_String == null)
            {
                ErrorBox.getInstance().Show("Блюдо из меню не выбрано");
                return;
            }


            for(int i=0;i< Order_Strings.Count();i++)
            {
                if(Order_Strings[i].FoodId == menu_String.FoodId)
                {
                    Menu_string o = Order_Strings[i];
                    Order_Strings[i] = new Menu_string() { Count = o.Count + ViewModel.Count, Food = o.Food, FoodId = o.FoodId, Id = o.Id };
                    ViewModel.Order = new ObservableCollection<Menu_string>(Order_Strings);
                    return;
                }
            }

            menu_String.Count = ViewModel.Count;
            Order_Strings.Add(menu_String);
            ViewModel.Order = new ObservableCollection<Menu_string>(Order_Strings);
        }



        private void Button_Click_2(object sender, RoutedEventArgs e) // Confirm order
        {
            if(Order_Strings.Count() == 0)
            {
                ErrorBox.getInstance().Show("Заказ пуст");
                return;
            }

            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                Order_status? ord_status = bd.Order_statuses?.Where(u => u.Title == ConfigController.getInstance().OrderProcessing).FirstOrDefault();
                Personal? curr_emp = bd.Personals?.Where(u => u.id == Curr_Emp.id).FirstOrDefault();
                Classes.Models.BDModels.Table? tab = bd.Tables?.Where(u => u.Id == table.Id).FirstOrDefault();

                if (ord_status == null || curr_emp == null || tab == null)
                {
                    ErrorBox.getInstance().Show("Ошибка связи программы и бд");
                    return;
                }

                bd.Orders?.Add(
                    new Order()
                    {
                        Date = string.Format("{0:dd.MM.yyyy}", DateTime.Now),
                        Time = string.Format("{0:HH:mm:ss}", DateTime.Now),
                        Order_status = ord_status,
                        Order_StatusId = ord_status.ID,

                        Personal = curr_emp,
                        PersonalId = curr_emp.id,

                        Table = tab,
                        TableId = tab.Id,
                    });

                bd.SaveChanges();

                Order? ord = bd.Orders?.OrderBy(u => u.Id).Last();
                Order_string_status? oss = bd.Order_string_statuses?.Where(u => u.Title == ConfigController.getInstance().OrderStringNotDone).FirstOrDefault();

                if (ord == null || oss == null)
                {
                    ErrorBox.getInstance().Show("Ошибка при добавлении заказа");
                    return;
                }


                for(int i = 0; i < Order_Strings.Count(); i++)
                {
                    Food? food = bd.Foods?.Where(u => u.Id == Order_Strings[i].FoodId).FirstOrDefault();

                    if(food != null)
                    bd.Order_strings?.Add(new Order_string() 
                    { 
                        CookPers = null, 
                        CookPersId = null, 
                        Food = food, 
                        FoodId = food.Id,
                        Food_count = Order_Strings[i].Count,
                        Order = ord,
                        OrderId = ord.Id,
                        Order_string_status = oss, 
                        Order_String_StatusId = oss.Id, 
                    });
                }

                bd.SaveChanges();

                PageController.getInstance()?.Goto(new WaiterPage1(Curr_Emp));

            }


        }




        private void Button_Click_3(object sender, RoutedEventArgs e) // clear order strings
        {
            ViewModel.Order.Clear();
            Order_Strings.Clear();
        }


        private void Button_Click_4(object sender, RoutedEventArgs e) // clear order strings
        {
            Button? button = sender as Button;
            string? tag = button?.Tag.ToString();
            if (tag == null) return;
            if (tag == "+") ViewModel.Add();
            else ViewModel.Sub();
        }

    }
}
