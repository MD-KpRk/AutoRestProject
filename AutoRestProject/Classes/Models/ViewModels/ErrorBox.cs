using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AutoRestProject.ViewModels
{
    public class ErrorBox : INotifyPropertyChanged
    {
        private static ErrorBox? instance;
        public static ErrorBox getInstance()
        {
            if(instance == null)
                instance = new ErrorBox();
            return instance;
        }


        string message = "";
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
