using AutoRestProject.Classes.Models.BDModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace AutoRestProject.Classes.Models.ViewModels
{
    public class OrderStringUserControlViewModel : INotifyPropertyChanged
    {

        string orderString = "", orderNum = "", currTime = "", orderPers = "";

        public Order_string Order_String;
        DispatcherTimer timer = new DispatcherTimer();

        SolidColorBrush brush = new SolidColorBrush(Colors.Black);

        public SolidColorBrush TimerBrush
        {
            get { return brush; }
            set
            {
                brush = value;
                OnPropertyChanged("TimerBrush");
            }
        }

        public string CurrTime
        {
            get { return currTime; }
            set
            {
                currTime = value;
                OnPropertyChanged("CurrTime");
            }
        }

        public string OrderPers
        {
            get { return orderPers; }
            set
            {
                orderPers = value;
                OnPropertyChanged("OrderPers");
            }
        }



        public OrderStringUserControlViewModel(Order_string order_String)
        {
            this.Order_String = order_String;
            timer.Interval = TimeSpan.FromSeconds(1);
            UpdateTimer();
        }

        void timer_Tick(object? sender, EventArgs e)
        {
            UpdateTimer();
        }

        public void UpdateTimer()
        {
            string[] date = Order_String.Order.Date.Split('.');
            string[] time = Order_String.Order.Time.Split(':');

            DateTime dat = new DateTime(Convert.ToInt32(date[2]), Convert.ToInt32(date[1]), Convert.ToInt32(date[0]), 
                Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), Convert.ToInt32(time[2]));

            string currtime =  (DateTime.Now.Subtract(dat).Days*24 + DateTime.Now.Subtract(dat).Hours).ToString() + 
                ":" + DateTime.Now.Subtract(dat).Minutes + ":" + DateTime.Now.Subtract(dat).Seconds;

            SetTimerColor((int)DateTime.Now.Subtract(dat).TotalSeconds);
            CurrTime = currtime;
        }

        void SetTimerColor(int Seconds)
        {
            if (Seconds < ConfigController.getInstance().GoodTimeSecond)
                TimerBrush = new SolidColorBrush(Colors.Green);
            else if (Seconds < ConfigController.getInstance().AverageTimeSecond)
                TimerBrush = new SolidColorBrush(Colors.Orange);
            else if (Seconds < ConfigController.getInstance().BadTimeSecond)
                TimerBrush = new SolidColorBrush(Colors.Red);
            else
                TimerBrush = new SolidColorBrush(Colors.DarkRed);
        }


        public string OrderString
        {
            get => orderString;
            set
            {
                orderString = value;
                OnPropertyChanged("OrderString");
            }
        }

        public string OrderNum
        {
            get => "#"+orderNum;
            set
            {
                orderNum = value;
                OnPropertyChanged("OrderNum");
            }
        }




        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
