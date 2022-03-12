using AutoRestProject.Classes.Models.BDModels;
using AutoRestProject.Classes.Models.ViewModels;
using System.Drawing.Printing;
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
    class Goods
    {
        public string name;
        public int code;
        public double price;
        public Goods(string name, int code, double price)
        {
            this.name = name;
            this.code = code;
            this.price = price;
        }
    }

    public partial class CheckPage : Page
    {
        Personal Emp;
        Order Order;
        CheckPageViewModel ViewModel = new CheckPageViewModel();
        public CheckPage(Personal emp, Order order)
        {
            Order = order;
            Emp = emp;
            ViewModel.Update(Order);
            DataContext = ViewModel;
            InitializeComponent();
        }

        string print = "";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PageController.getInstance()?.Goto(new WaiterPage1(Emp));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                // Создать текст

                // Поместить его в TextBlock
                TextBlock tb = new TextBlock();

                // Использовать поля для получения рамки страницы
                tb.Margin = new Thickness(40);
                //tb.TextWrapping = TextWrapping.Wrap;

                // Установить размер элемента
                Size pageSize = new Size(400, printDialog.PrintableAreaHeight);
                tb.Measure(pageSize);
                tb.Arrange(new Rect(60, 60, 400, pageSize.Height));
                tb.TextWrapping = TextWrapping.Wrap;
                tb.Width = 400;


                //stack.Width = 400;
                //stack.HorizontalAlignment = HorizontalAlignment.Left;
                //stack.LayoutTransform = new ScaleTransform(0.3,0.3);


                int counter = 0;
                double sum = 0;
                var builder = new StringBuilder();
                var buyerList = new List<Goods>
                {
                new Goods("Вода минеральная 0.5л", 8433, 44),
                new Goods("Батончик СНИНЕРС 140гр", 25499, 39),
                new Goods("Хлеб нарезной Добрыня", 30996, 29),
                new Goods("котлета синяя с мармеладным приколом (мрц 71)", 30934, 71)};
                builder.AppendLine($"{"".PadRight(25, ' ')}Касса №2");

                foreach (var product in buyerList)
                {
                    counter++;
                    sum += product.price;
                    builder.AppendLine($"{counter}.{product.name}");
                    builder.AppendLine($"  Код:{product.code}");
                    builder.AppendLine($"  Стоимость{"".PadRight(40 - product.price.ToString().Length, '.')}{product.price}");
                }

                builder.AppendLine("".PadRight(51, '='));
                builder.AppendLine($"Всего{"".PadRight(46 - sum.ToString().Length, '.')}{sum}");
                builder.AppendLine($"Скидка{"".PadRight(42, ' ')}15%");
                sum -= Math.Round(sum / 100 * 15, 2);
                builder.AppendLine($"Итог{"".PadRight(47 - sum.ToString().Length, ' ')}{sum}");


                tb.Inlines.Add(builder.ToString());

                printDialog.PrintVisual(tb, "Распечатываем текст");
            }
        }


        
    }
}
