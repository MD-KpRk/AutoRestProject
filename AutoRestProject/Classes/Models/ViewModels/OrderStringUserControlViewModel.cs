using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoRestProject.Classes.Models.ViewModels
{
    class OrderStringUserControlViewModel : INotifyPropertyChanged
    {

        string orderString = "";

        public string OrderString
        {
            get => orderString;
            set
            {
                orderString = value;
                OnPropertyChanged("OrderString");
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
