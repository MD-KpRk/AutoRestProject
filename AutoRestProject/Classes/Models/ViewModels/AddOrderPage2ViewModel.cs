using AutoRestProject.Classes.Models.BDModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoRestProject.Classes.Models.ViewModels
{
    internal class AddOrderPage2ViewModel : INotifyPropertyChanged
    {
        string currPersName = "", currPersPos = "";
        ObservableCollection<Menu_string> menu = new ObservableCollection<Menu_string>();
        ObservableCollection<Menu_string> order = new ObservableCollection<Menu_string>();

        int count = 1;
        public string CurrPersName
        {
            get => currPersName;
            set
            {
                currPersName = value;
                OnPropertyChanged("CurrPersName");
            }
        }

        public string CurrPersPos
        {
            get => currPersPos;
            set
            {
                currPersPos = value;
                OnPropertyChanged("CurrPersPos");
            }
        }

        public ObservableCollection<Menu_string> Menu
        {
            get { return menu; }
            set
            {
                menu = value;
                OnPropertyChanged("Menu");
            }
        }

        public ObservableCollection<Menu_string> Order
        {
            get { return order; }
            set
            {
                order = value;
                OnPropertyChanged("Order");
            }
        }

        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                OnPropertyChanged("Count");
            }
        }

        public void Sub()
        {
            if (count - 1 > 0) count--;
            OnPropertyChanged("Count");
        }
        public void Add()
        {
            count++;
            OnPropertyChanged("Count");
        }




        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
