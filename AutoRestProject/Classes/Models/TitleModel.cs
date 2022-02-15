using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace AutoRestProject
{
    /// <summary>
    /// Класс для описания модели заголовка окна
    /// </summary>
    public class TitleModel : INotifyPropertyChanged
    {
        private static TitleModel? instance;
        string programtitle;
        string date;
        DispatcherTimer timer = new DispatcherTimer();

        public static TitleModel getInstance()
        {
            if (instance == null)
                instance = new TitleModel();
            return instance;
        }

        TitleModel()
        {
            // Json настройка
            programtitle = "Title";
            date = "";
            SetCurrentDate();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        public string ProgramTitle
        {
            get => programtitle;
            set
            {
                programtitle = value;
                OnPropertyChanged("ProgramTitle");
            }
        }
        public string Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        void timer_Tick(object? sender, EventArgs e) => SetCurrentDate();
        void SetCurrentDate() =>
            Date = DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString();

    }
}
