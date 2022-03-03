using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoRestProject.ViewModels
{
    class AuthWindowViewModel : INotifyPropertyChanged
    {
        string password = "";


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
                    stringBuilder.Append('∗');
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
            if (Password.Length == 0) return;
            StarPassword = Password.Substring(0, Password.Length - 1);
        }

        public void AddPassDigit(int digit)
        {
            if (Password.Length >= 8) return;
            StarPassword = Password + digit;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
