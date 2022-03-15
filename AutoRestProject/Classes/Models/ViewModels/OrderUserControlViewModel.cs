using AutoRestProject.Classes.Models.BDModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace AutoRestProject.ViewModels
{
    public class OrderUserControlViewModel : INotifyPropertyChanged
    {
        int orderNum = 0, tableNum;
        string orderStatus = "", empName = "";

        public string OrderStrinDone
        {
            get
            {
                string? str = ConfigController.getInstance()?.OrderStringDone;
                if (str == null)
                {
                    return "ER";
                }

                return str;
            }
        }
        public string OrderStringNotDone
        {
            get
            {
                string? str = ConfigController.getInstance()?.OrderStringNotDone;
                if (str == null)
                {
                    return "ER";
                }

                return str;
            }
        }
        public string OrderStringProcessing
        {
            get
            {
                string? str = ConfigController.getInstance()?.OrderStringProcessing;
                if (str == null)
                {
                    return "ER";
                }

                return str;
            }
        }
        public ObservableCollection<Order_string> Order_Strings { get; set; } = new ObservableCollection<Order_string>();

        public SolidColorBrush Color
        {
            get
            {
                ConfigController config = ConfigController.getInstance();

                if (orderStatus == config.OrderProcessing)
                {
                    return (SolidColorBrush)Application.Current.Resources["C_Green"];
                }

                if (orderStatus == config.OrderWaitingPayment)
                {
                    return (SolidColorBrush)Application.Current.Resources["C_Orange"];
                }

                return new SolidColorBrush(Colors.Red);
            }
        }
        public int OrderNum
        {
            get => orderNum;
            set
            {
                orderNum = value;
                OnPropertyChanged("OrderNum");
            }
        }

        public int TableNum
        {
            get => tableNum;
            set
            {
                tableNum = value;
                OnPropertyChanged("TableNum");
            }
        }

        public string OrderStatus
        {
            get => orderStatus;
            set
            {
                orderStatus = value;
                OnPropertyChanged("OrderStatus");
            }
        }

        public string EmpName
        {
            get => empName;
            set
            {
                empName = value;
                OnPropertyChanged("EmpName");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
