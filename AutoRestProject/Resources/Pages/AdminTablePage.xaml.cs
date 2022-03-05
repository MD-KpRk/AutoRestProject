using AutoRestProject.Classes.Models.BDModels;
using AutoRestProject.Classes.Models.ViewModels;
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
    /// Логика взаимодействия для AdminTablePage.xaml
    /// </summary>
    public partial class AdminTablePage : Page
    {
        Personal Emp;
        AdminTablePageViewModel ViewModel = new AdminTablePageViewModel();

        public AdminTablePage(Personal Emp)
        {
            this.Emp = Emp;
            ViewModel.PersPos = Emp.Position.Title;
            ViewModel.PersName = Emp.First_name + " " + Emp.Second_name;
            ViewModel.Update();
            DataContext = ViewModel;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Назад
        {
            PageController.getInstance()?.Goto(new AdminPage1(Emp));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // Забронировать
        {
            Classes.Models.BDModels.Table? table = dg1.SelectedItem as Classes.Models.BDModels.Table;
            if(table == null)
            {
                ErrorBox.getInstance().Show("Свободный столик не выбран");
                return;
            }

            SetTableStatus(table, ConfigController.getInstance().TableStatusReserved);

            ViewModel.Update();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // Освободить
        {
            Classes.Models.BDModels.Table? table = dg2.SelectedItem as Classes.Models.BDModels.Table;
            if (table == null)
            {
                ErrorBox.getInstance().Show("Столик не выбран");
                return;
            }

            SetTableStatus(table, ConfigController.getInstance().TableStatusFree);

            ViewModel.Update();

        }

        void SetTableStatus(Classes.Models.BDModels.Table table, string? status_str)
        {
            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                Classes.Models.BDModels.Table? tabcontext = bd.Tables?.Where(u => table.Id == u.Id).FirstOrDefault();
                Table_status? tabstatuscontext = bd.Table_statuses?.Where(u => u.Title == status_str).FirstOrDefault();
                Table_status? tabstatusbusycontext = bd.Table_statuses?.Where(u => u.Title == ConfigController.getInstance().TableStatusBusy).FirstOrDefault();
                if (tabcontext == null || tabstatuscontext == null || tabstatusbusycontext == null)
                {
                    ErrorBox.getInstance().Show("Ошибка связи бд с программой");
                    return;
                }
                if(tabcontext.Table_StatusID == tabstatusbusycontext.Id)
                {
                    ErrorBox.getInstance().Show("Вы не можете освободить занятый людьми стол. Чтобы этот стол освободился нужно закрыть заказ");
                    return;
                }
                tabcontext.Table_StatusID = tabstatuscontext.Id;
                bd.SaveChanges();
            }

        }


    }
}
