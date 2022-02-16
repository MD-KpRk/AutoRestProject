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
        int intpass;

        public string Password
        {
            get { return password; }
            set 
            { 
                OnPropertyChanged("Password");
                password = value; 
            }
        }

        public AuthWindowViewModel()
        { }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
