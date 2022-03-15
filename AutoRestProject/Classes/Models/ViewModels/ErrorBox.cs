using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AutoRestProject
{
    public class ErrorBox : INotifyPropertyChanged
    {
        private static ErrorBox? instance;
        public static ErrorBox getInstance()
        {
            if (instance == null)
            {
                instance = new ErrorBox();
            }

            return instance;
        }


        string message = "", header = "";
        Visibility visibility = Visibility.Collapsed;
        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        public string Header
        {
            get => header;
            set
            {
                header = value;
                OnPropertyChanged("Header");
            }
        }

        public Visibility Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        ErrorBox() { }

        public void Show(string message)
        {
            Visibility = Visibility.Visible;
            Message = message;
            Header = "Ошибка";
        }

        public void ShowInfo(string message)
        {
            Visibility = Visibility.Visible;
            Message = message;
            Header = "Информация";
        }

        public void Hide()
        {
            Visibility = Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
