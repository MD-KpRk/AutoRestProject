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
    public class WaiterPage1ViewModel : INotifyPropertyChanged
    {
        string persname = "", perspos = "";
        string orderpersname = "", orderperspos = "";

        int orderNum = 0;

        Visibility visibility = Visibility.Collapsed;
        public Visibility Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        public int OrderNum
        {
            get => orderNum;
            set
            {
                orderNum = value;
                OnPropertyChanged("OrderNum");
            }
        }


        public void PanelClose()
        {
            Visibility = Visibility.Collapsed;
        }

        public void PanelShow()
        {
            Visibility = Visibility.Visible;
        }

        public string PersName
        {
            get => persname;
            set
            {
                persname = value;
                OnPropertyChanged("PersName");
            }
        }
        public string OrderPersName
        {
            get => orderpersname;
            set
            {
                orderpersname = value;
                OnPropertyChanged("OrderPersName");
            }
        }

        public string OrderPersPos
        {
            get => orderperspos;
            set
            {
                orderperspos = value;
                OnPropertyChanged("OrderPersPos");
            }
        }

        public string PersPos
        {
            get => perspos;
            set
            {
                perspos = value;
                OnPropertyChanged("PersPos");
            }
        }

        public void SetPerson(string name, string position)
        {
            PersName = name;
            PersPos = position;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
