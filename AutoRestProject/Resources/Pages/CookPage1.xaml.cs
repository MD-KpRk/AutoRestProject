using AutoRestProject.Classes.Models.BDModels;
using AutoRestProject.Resources.UserControls;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для CookPage1.xaml
    /// </summary>
    public partial class CookPage1 : Page
    {
        public bool CanScroll { get; set; } = true;

        public CookPage1()
        {
            InitializeComponent();

            MessageBox.Show("awd");
            //Получение всех заказов
            //Получение всех строк заказов из заказов
            //Создание  юзер контрола с передачей данных по строке заказа

            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
               var list = bd.Orders?
                    .Include(u => u.Table)
                    .Include(u => u.Personal)
                        .ThenInclude(u => u.Position)
                    .Include(u => u.Order_status)
                    .Include(u => u.Order_strings)
                        .ThenInclude(u => u.Food)
                    .ToList();





                StringBuilder str = new StringBuilder();
                foreach (var o in list)
                {
                    //var order_strgs = bd.Order_strings?.Where(u => u.OrderId == o.Id)
                    //    .Include(u=>u.CookPers).ToList();

                    foreach(var a in o.Order_strings)
                    {
                        if (a.CookPers == null)
                        {
                            MessageBox.Show(a.Food.Title + " не готовится");
                        }
                        else
                        {
                            MessageBox.Show(a.Food.Title + " готовится " + a.CookPers.First_name);
                        }
                        //MessageBox.Show(o.Order_strings.Count().ToString() +"   "+ a.Food.Title);
                    }



                    //if (o.Order_strings.Count() != 0)
                    //{
                    //    foreach (Order_string a in o.Order_strings)
                    //    {

                    //        str.Append(a.Food.Title + a.CookPers.First_name);
                    //    }
                    //    MessageBox.Show(str.ToString());
                    //}
                    //else
                    //    MessageBox.Show("Заказ пуст");
                }


            }


            //OrderStringUserControl[] os = new OrderStringUserControl[10];

            //for(int i =0;i<os.Length;i++)
            //{
            //    os[i] = new OrderStringUserControl(this);
            //    stack.Children.Add(os[i]);

            //}

        }




        // Scroll Panel
        Point scrollMousePoint = new Point();
        double hOff = 1;
        private void scrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            scrollMousePoint = e.GetPosition(scrollviewer);
            hOff = scrollviewer.VerticalOffset;
            if (CanScroll == true)
                scrollviewer.CaptureMouse();
        }
        private void scrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (scrollviewer.IsMouseCaptured)
                scrollviewer.ScrollToVerticalOffset(hOff + (scrollMousePoint.Y - e.GetPosition(scrollviewer).Y));
        }
        private void scrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollviewer.ReleaseMouseCapture();
        }

        private void scrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollviewer.ScrollToHorizontalOffset(scrollviewer.VerticalOffset + e.Delta);
        }
    }
}
