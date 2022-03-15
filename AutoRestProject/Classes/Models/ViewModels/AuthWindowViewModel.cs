using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutoRestProject.ViewModels
{
    class AuthWindowViewModel : INotifyPropertyChanged
    {
        string password = "";

        int totalPers = 0, orders = 0, bluds = 0, tables = 0, menus = 0;

        public int TotalPers
        {
            get { return totalPers; }
            set
            {
                totalPers = value;
                OnPropertyChanged("TotalPers");
            }
        }

        public int Menus
        {
            get { return menus; }
            set
            {
                menus = value;
                OnPropertyChanged("Menus");
            }
        }

        public int Tables
        {
            get { return tables; }
            set
            {
                tables = value;
                OnPropertyChanged("Tables");
            }
        }

        public int Orders
        {
            get { return orders; }
            set
            {
                orders = value;
                OnPropertyChanged("Orders");
            }
        }

        public int Bluds
        {
            get { return bluds; }
            set
            {
                bluds = value;
                OnPropertyChanged("Bluds");
            }
        }

        public string Password
        {
            get => password;
            set => password = value;
        }

        public string StarPassword
        {
            get
            {
                StringBuilder stringBuilder = new();
                for (int i = 0; i < Password.Length; i++)
                {
                    stringBuilder.Append('∗');
                }

                return stringBuilder.ToString();
            }
            set
            {
                Password = value;
                OnPropertyChanged("StarPassword");
            }
        }

        public void RemovePassDigit()
        {
            if (Password.Length == 0)
            {
                return;
            }

            StarPassword = Password.Substring(0, Password.Length - 1);
        }

        public void AddPassDigit(int digit)
        {
            if (Password.Length >= 8)
            {
                return;
            }

            StarPassword = Password + digit;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
