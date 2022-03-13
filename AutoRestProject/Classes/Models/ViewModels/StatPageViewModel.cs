using AutoRestProject.Classes.Models.BDModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoRestProject.Classes.Models.ViewModels
{

    class StatClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Count { get; set; } = 0;

        public static List<StatClass> AddToStat(List<StatClass> list, Personal? pers, int plus = 0)
        {
            if (pers == null) return list;
            bool ets = list.Exists(u => u.Id == pers.id);
            if (ets == true)
            {
                for(int i=0;i< list.Count();i++)
                    if(list[i].Id == pers.id)
                        list[i].Count += plus;
            }
            else
            {
                list.Add(new StatClass() { Id = pers.id, Count = plus , Name = pers.Second_name +" "+ pers.First_name});
            }

            return list;

        }
    }

    class StatPageViewModel : INotifyPropertyChanged
    {

        ObservableCollection<StatClass> povars = new ObservableCollection<StatClass>();
        public ObservableCollection<StatClass> Povars
        {
            get { return povars; }
            set
            {
                povars = value;
                OnPropertyChanged("Povars");
            }
        }

        ObservableCollection<StatClass> waiters = new ObservableCollection<StatClass>();
        public ObservableCollection<StatClass> Waiters
        {
            get { return waiters; }
            set
            {
                waiters = value;
                OnPropertyChanged("Waiters");
            }
        }

        public void Update()
        {
            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                string datetime = string.Format("{0:dd.MM.yyyy}", DateTime.Now);
                List<Order_string>? ostr = 
                    bd.Order_strings?
                    .Include(u => u.Order.Personal.Position)?
                    .Include(u => u.CookPers)
                    .Include(u => u.Order_string_status)
                    .Where(u => u.Order.Date == datetime)
                    .Where(u => u.Order_string_status.Title == ConfigController.getInstance().OrderStringDone).ToList();
                List<Personal>? pers = bd.Personals?.Include(u => u.Position)
                    .Where(u=>u.Position.Title == ConfigController.getInstance().Cook || u.Position.Title == ConfigController.getInstance().Chief).ToList();

                if (ostr == null || pers == null) return;

                List<StatClass> statlist = new List<StatClass>();

                for (int i = 0; i < ostr.Count; i++)
                {
                    statlist = StatClass.AddToStat(statlist, ostr[i].CookPers,1);
                }

                for(int i = 0; i < pers.Count; i++)
                {
                    statlist = StatClass.AddToStat(statlist, pers[i],0);
                }


                MessageBox.Show(ostr.Count.ToString());
                Povars = new ObservableCollection<StatClass>(statlist);
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
