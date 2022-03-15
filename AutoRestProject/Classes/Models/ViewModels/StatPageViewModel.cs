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

    class StatClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Count { get; set; } = 0;

        public static List<StatClass> AddToStat(List<StatClass> list, Personal? pers, int plus = 0)
        {
            if (pers == null)
            {
                return list;
            }

            bool ets = list.Exists(u => u.Id == pers.id);
            if (ets == true)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    if (list[i].Id == pers.id)
                    {
                        list[i].Count += plus;
                    }
                }
            }
            else
            {
                list.Add(new StatClass() { Id = pers.id, Count = plus, Name = pers.Second_name + " " + pers.First_name });
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

                List<Order>? otr =
                    bd.Orders?
                    .Include(u => u.Personal)
                    .Where(u => u.Date == datetime)
                    .Where(u => u.Order_status.Title == ConfigController.getInstance().OrderDone).ToList();

                List<Personal>? povars2 = bd.Personals?.Include(u => u.Position)
                    .Where(u => u.Position.Title == ConfigController.getInstance().Cook || u.Position.Title == ConfigController.getInstance().Chief).ToList();

                List<Personal>? waiters2 = bd.Personals?.Include(u => u.Position)
                    .Where(u => u.Position.Title == ConfigController.getInstance().Waiter || u.Position.Title == ConfigController.getInstance().Chief).ToList();

                if (ostr == null || otr == null || povars2 == null || waiters2 == null)
                {
                    return;
                }

                List<StatClass> statpovarlist = new List<StatClass>();
                List<StatClass> statwaiterlist = new List<StatClass>();

                for (int i = 0; i < ostr.Count; i++)
                {
                    statpovarlist = StatClass.AddToStat(statpovarlist, ostr[i].CookPers, 1);
                }

                for (int i = 0; i < otr.Count; i++)
                {
                    statwaiterlist = StatClass.AddToStat(statwaiterlist, otr[i].Personal, 1);
                }
                for (int i = 0; i < povars2.Count; i++)
                {
                    statpovarlist = StatClass.AddToStat(statpovarlist, povars2[i], 0);
                }

                for (int i = 0; i < waiters2.Count; i++)
                {
                    statwaiterlist = StatClass.AddToStat(statwaiterlist, waiters2[i], 0);
                }
                Povars = new ObservableCollection<StatClass>(statpovarlist);
                Waiters = new ObservableCollection<StatClass>(statwaiterlist);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
