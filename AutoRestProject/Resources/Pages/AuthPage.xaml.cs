using AutoRestProject.Classes.Models.BDModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        ViewModels.AuthWindowViewModel ViewModel = new();

        public AuthPage()
        {
            DataContext = ViewModel;
            Update();
            InitializeComponent();
        }

        public void Update()
        {
            using(AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                int? pcount = bd.Personals?.Count();
                int? ocount = bd.Orders?.Include(u=>u.Order_status).Where(u => u.Order_status.Title != ConfigController.getInstance().OrderDone).Count();
                int? bcount = bd.Order_strings?.Include(u=>u.Order_string_status).Include(u=>u.CookPers).Where(u => u.Order_string_status.Title != ConfigController.getInstance().OrderStringDone && u.CookPers == null).Count();
                int? tcount = bd.Tables?.Include(u => u.Table_Status).Where(u => u.Table_Status.Title == ConfigController.getInstance().TableStatusFree).Count();
                int? mcount = bd.Menu_strings?.Count();
                if (pcount == null || ocount == null || bcount == null || tcount == null || mcount == null) return;

                ViewModel.TotalPers = (int)pcount;
                ViewModel.Orders = (int)ocount;
                ViewModel.Bluds = (int)bcount;
                ViewModel.Tables = (int)tcount;
                ViewModel.Menus = (int)mcount;

            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string tag = (string)((Button)sender).Tag;
            switch (tag)
            {
                case "Apply":
                    ApplyPass(ViewModel.Password);
                    return;

                case "Delete":
                    ViewModel.RemovePassDigit();
                    return;
            }
            ViewModel.AddPassDigit(Convert.ToInt32(tag));
        }

        void ApplyPass(string Password)
        {
            using (AutoRestBDContext db = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                if (string.IsNullOrEmpty(ViewModel.Password))
                {
                    ErrorBox.getInstance().Show("Введите PIN код");
                    return;
                }

                Personal? personal = db.Personals?
                    .Include(u => u.Position)
                    .FirstOrDefault(pers => pers.Pin == Convert.ToInt32(Password));


                if (personal == null)
                {
                    ErrorBox.getInstance().Show("Не найден сотрудник с таким PIN кодом");
                    return;
                }

                string PosTitle = personal.Position.Title;

                if (PosTitle == ConfigController.getInstance().Waiter)
                {
                    PageController.getInstance()?.Goto(new WaiterPage1(personal));
                    return;
                }
                if (PosTitle == ConfigController.getInstance().Chief)
                {
                    PageController.getInstance()?.Goto(new ChiefPage1(personal));
                    return;
                }
                if (PosTitle == ConfigController.getInstance().Admin)
                {
                    PageController.getInstance()?.Goto(new AdminPage1(personal));
                    return;
                }
                if (PosTitle == ConfigController.getInstance().Cook)
                {
                    PageController.getInstance()?.Goto(new CookPage1(personal));
                    return;
                }


            }

        }


    }
}
