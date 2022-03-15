using AutoRestProject.Classes.Models.BDModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace AutoRestProject.Classes.Models.ViewModels
{
    internal class CheckPageViewModel : INotifyPropertyChanged
    {
        string summa = "", restName = "";
        ObservableCollection<Order_string> strings = new ObservableCollection<Order_string>();
        public ObservableCollection<Order_string> Strings
        {
            get { return strings; }
            set
            {
                strings = value;
                OnPropertyChanged("Strings");
            }
        }

        public string Summa
        {
            get { return summa; }
            set
            {
                summa = value;
                OnPropertyChanged("Summa");
            }
        }

        public string RestName
        {
            get { return restName; }
            set
            {
                restName = value;
                OnPropertyChanged("RestName");
            }
        }

        public void Update(Order ord)
        {
            List<Order_string>? list;
            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                summa = "";
                list = bd.Order_strings?.Where(u => u.OrderId == ord.Id).Include(u => u.Food).ToList();
                if (list == null)
                {
                    return;
                }

                double total = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    double iter = Math.Round(list[i].Food.Price * list[i].Food_count, 2);
                    total += iter;
                    list[i].Price = iter + " BYN";
                }
                Summa = Math.Round(total, 2) + " BYN";
            }
            Strings = new ObservableCollection<Order_string>(list);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
