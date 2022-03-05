using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AutoRestProject.Classes.Models.BDModels;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;

namespace AutoRestProject.Resources.Pages
{
    /// <summary>
    /// Логика взаимодействия для WaiterPage1.xaml
    /// </summary>
    public partial class WaiterPage1 : Page
    {
        ViewModels.WaiterPage1ViewModel ViewModel = new();
        public bool CanScroll { get; set; } = true;

        Personal CurrentUser;
        Order? CurrentOrder;

        public WaiterPage1(Personal emp)
        {
            CurrentUser = emp;
            ViewModel.SetPerson(emp.First_name + " " + emp.Second_name,emp.Position.Title);
            DataContext = ViewModel;

            InitializeComponent();

            Update();

        }

        public void Update()
        {
            stack.Children.Clear();

            List<Order>? list = new List<Order>();

            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                list = bd.Orders?
                    .Include(u => u.Table)
                    .Include(u => u.Personal)
                        .ThenInclude(u => u.Position)
                    .Include(u => u.Order_status)
                    .Include(u => u.Order_strings)
                        .ThenInclude(u => u.Food)
                    .ToList();

                List<Order_string>? l22 = bd.Order_strings?.Include(u => u.Order_string_status).ToList();


                for (int i = 0; i < list?.Count(); i++)
                {
                    if(list[i].Order_status.Title != ConfigController.getInstance().OrderDone)
                        stack.Children.Add(new UserControls.OrderUserControl(this, list[i]));
                }

            }
        }




        // Scroll Panel
        Point scrollMousePoint = new Point();
        double hOff = 1;
        private void scrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            scrollMousePoint = e.GetPosition(scrollviewer);
            hOff = scrollviewer.HorizontalOffset;
            if(CanScroll == true)
                scrollviewer.CaptureMouse();
        }
        private void scrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (scrollviewer.IsMouseCaptured)
                scrollviewer.ScrollToHorizontalOffset(hOff + (scrollMousePoint.X - e.GetPosition(scrollviewer).X));
        }
        private void scrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollviewer.ReleaseMouseCapture();
        }

        private void scrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollviewer.ScrollToHorizontalOffset(scrollviewer.HorizontalOffset + e.Delta);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PanelClose();
        }

        public void ShowPanel(Order order)
        {
            CurrentOrder = order;
            ViewModel.OrderNum = order.Id;
            ViewModel.OrderPersName = order.Personal.First_name + " " + order.Personal.Second_name;
            ViewModel.OrderPersPos = order.Personal.Position.Title;
            ViewModel.PanelShow();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // + заказ
        {
            PageController.getInstance()?.Goto(new AddOrderPage1(CurrentUser));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // Выход
        {
            PageController.getInstance()?.Goto(new AuthPage());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) // в обработке
        {
            ChangeOrderStatus(ConfigController.getInstance().OrderProcessing);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) // в ожидании оплаты
        {
            ViewModel.PanelClose();
            ChangeOrderStatus(ConfigController.getInstance().OrderWaitingPayment);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e) // Оплачен
        {
            ViewModel.PanelClose();

            if(CurrentOrder == null)
            {
                ErrorBox.getInstance().Show("Ошибка связи программы и бд");
                return;
            }

            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                Classes.Models.BDModels.Table? table = bd.Tables?.Where(u => u.Id == CurrentOrder.TableId).FirstOrDefault();
                int? status_freeid = bd.Table_statuses?.Where(u => u.Title == ConfigController.getInstance().TableStatusFree).FirstOrDefault()?.Id;

                if(table == null || status_freeid == null)
                {
                    ChangeOrderStatus(ConfigController.getInstance().OrderDone);
                    return;
                }

                table.Table_StatusID = (int)status_freeid;
                bd.SaveChanges();
            }

            ChangeOrderStatus(ConfigController.getInstance().OrderDone);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e) // Удалить заказ
        {
            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                if (CurrentOrder == null)
                {
                    ErrorBox.getInstance().Show("Ошибка связи программы и бд");
                    return;
                }

                Order? order = bd.Orders?.Where(u => u.Id == CurrentOrder.Id).FirstOrDefault();

                if (order == null)
                {
                    ErrorBox.getInstance().Show("Ошибка связи программы и бд");
                    return;
                }

                List<Order_string>? list = bd.Order_strings?.Where(u=> u.OrderId == order.Id).ToList();

                if (order == null || list == null)
                {
                    ErrorBox.getInstance().Show("Ошибка связи программы и бд");
                    return;
                }

                bd.Order_strings?.RemoveRange(list);
                bd.Orders?.Remove(order);
                bd.SaveChanges();
                ViewModel.PanelClose();
                Update();

            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e) // Пополнить заказ ?
        {
            List<Order_string>? list = new List<Order_string>();
            if (CurrentOrder == null)
            {
                ErrorBox.getInstance().Show("Ошибка связи программы и бд");
                return;
            }
            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                list = bd.Order_strings?.Include(u => u.Food).Where(u => u.OrderId == CurrentOrder.Id).ToList();
            }

            if ( CurrentUser == null || list == null)
            {
                ErrorBox.getInstance().Show("Ошибка связи программы и бд");
                return;
            }

            PageController.getInstance()?.Goto(new AddOrderPage2(CurrentOrder.Table, CurrentUser, list));
        }

        private void Button_Click_8(object sender, RoutedEventArgs e) // Сгенерировать счёт
        {

        }



        void ChangeOrderStatus(string? status = "")
        {
            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                Order_status? ord = bd.Order_statuses?.Where(u => u.Title == status).FirstOrDefault();

                if (ord == null || CurrentOrder == null)
                {
                    ErrorBox.getInstance().Show("Ошибка связи программы и бд");
                    return;
                }
                Order? order = bd.Orders?.Where(u => u.Id == CurrentOrder.Id).FirstOrDefault();

                if (order == null)
                {
                    ErrorBox.getInstance().Show("Ошибка связи программы и бд");
                    return;
                }

                order.Order_status = ord;
                order.Order_StatusId = ord.ID;
                bd.SaveChanges();
                Update();
            }
        }
    }
}
