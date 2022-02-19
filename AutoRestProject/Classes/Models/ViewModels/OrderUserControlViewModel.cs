using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoRestProject.ViewModels
{
    public class OrderUserControlViewModel : INotifyPropertyChanged
    {
        int orderNum = 0;



        public int OrderNum
        {
            get => orderNum;
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
