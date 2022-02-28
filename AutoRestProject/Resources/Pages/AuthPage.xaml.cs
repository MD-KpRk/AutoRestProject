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
            InitializeComponent();
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


            }

        }


    }
}
