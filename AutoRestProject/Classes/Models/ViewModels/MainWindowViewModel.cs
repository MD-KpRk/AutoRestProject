using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace AutoRestProject.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        string programtitle;
        string date = "";
        DispatcherTimer timer = new DispatcherTimer();

        public ErrorBox ErrorBox { get; set; } = ErrorBox.getInstance();

        public MainWindowViewModel()
        {
            string? title = ConfigController.getInstance().OrgTitle;
            programtitle = title == null ? "" : title;
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        void timer_Tick(object? sender, EventArgs e) => SetCurrentDate();
        void SetCurrentDate() =>
            Date = DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString();

    }
}
