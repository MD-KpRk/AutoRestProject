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

        Personal Emp;

        public ChiefPage1(Personal personal)
        {
            Emp = personal;
            ViewModel.currPersPos = personal.Position.Title;
            ViewModel.currPersName = personal.First_name + " " + personal.Second_name;
            DataContext = ViewModel;

            InitializeComponent();
        }

        private void dg2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e) // выход
        {
            PageController.getInstance()?.Goto(new AuthPage());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // заказы
        {
            PageController.getInstance()?.Goto(new WaiterPage1(Emp));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // убрать из меню
        {
            Menu_string? menu_String = (dg1.SelectedItem as Menu_string);

            if(menu_String == null)
            {
                ErrorBox.getInstance().Show("Блюдо из меню не выбрано. Для выбора блюда нажмите на него в списке");
                return;
            }

            using (AutoRestBDContext db = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                try
                {
                    ViewModel.Menu_Strings.Remove(menu_String);
                    db.Menu_strings?.Remove(menu_String);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ErrorBox.getInstance().Show(ex.Message);
                }
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e) // добавить в меню
        {

            using (AutoRestBDContext db = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                Food? food = (dg2.SelectedItem as Food);
                if (food == null) return;
                //Food? foodfrombd = db.Foods?.Where(u => u.Id == food.Id).FirstOrDefault();

                //if (food == null)
                //{
                //    ErrorBox.getInstance().Show("Блюдо из ассортимента не выбрано. Для выбора блюда нажмите на него в списке");
                //    return;
                //}

                Menu_string menu_String = new Menu_string() { FoodId = food.Id};



                //try
                //{
                    db.Menu_strings?.Add(menu_String);
                    
                    db.SaveChanges();

                    ViewModel.UpdateMenuStrings();
                //}
                //catch (Exception ex)
                //{
                //    ErrorBox.getInstance().Show(ex.Message);
                //}
            }
        }
    }
}
