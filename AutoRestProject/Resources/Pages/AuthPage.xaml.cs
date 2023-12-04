using AutoRestProject.Classes.Models.BDModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AutoRestProject.Resources.Pages
{
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
            try
            {
                using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
                {
                    int? personalCount = bd.Personals?.Count();
                    int? ordersCount = bd.Orders?.Include(u => u.Order_status).Where(u => u.Order_status.Title != ConfigController.getInstance().OrderDone).Count();
                    int? bludsCount = bd.Order_strings?.Include(u => u.Order_string_status).Include(u => u.CookPers).Where(u => u.Order_string_status.Title != ConfigController.getInstance().OrderStringDone && u.CookPers == null).Count();
                    int? tablesCount = bd.Tables?.Include(u => u.Table_Status).Where(u => u.Table_Status.Title == ConfigController.getInstance().TableStatusFree).Count();
                    int? menusCount = bd.Menu_strings?.Count();
                    if (personalCount == null || ordersCount == null || bludsCount == null || tablesCount == null || menusCount == null)
                    {
                        return;
                    }
                    ViewModel.TotalPers = (int)personalCount;
                    ViewModel.Orders = (int)ordersCount;
                    ViewModel.Bluds = (int)bludsCount;
                    ViewModel.Tables = (int)tablesCount;
                    ViewModel.Menus = (int)menusCount;

                }
            }
            catch (Exception)
            {
                ErrorBox.getInstance().Show("Ошибка обновления информции");
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
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo("help.chm")
                {
                    UseShellExecute = true
                };
                p.Start();
            }
            catch (Exception)
            {
                ErrorBox.getInstance().Show("Ошибка при открытии справки");
            }
        }
    }
}
