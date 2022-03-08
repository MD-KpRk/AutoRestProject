using AutoRestProject.Classes.Models.BDModels;
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
    class AdminTablePageViewModel : INotifyPropertyChanged
    {
        string perspos = "", persname = "";
        int seats = 1;
        public ObservableCollection<Classes.Models.BDModels.Table> freetables = new ObservableCollection<Classes.Models.BDModels.Table>();
        public ObservableCollection<Classes.Models.BDModels.Table> reservedtables = new ObservableCollection<Classes.Models.BDModels.Table>();

        public int Seats
        {
            get { return seats; }
            set
            {
                seats = value;
                OnPropertyChanged("Seats");
            }
        }

        bool cb1 = false;

        public bool CB1
        {
            get { return cb1;}
            set
            {
                cb1 = value;
                OnPropertyChanged("CB1");
            }
        }


        public void Add()
        {
            Seats++;
            Update();
        }
        public void Sub()
        {
            if (Seats - 1 > 0) Seats--;
            Update();
        }

        public ObservableCollection<Classes.Models.BDModels.Table> FreeTables
        {
            get { return freetables; }
            set
            {
                freetables = value;
                OnPropertyChanged("FreeTables");
            }
        }

        public ObservableCollection<Classes.Models.BDModels.Table> ReservedTables
        {
            get { return reservedtables; }
            set
            {
                reservedtables = value;
                OnPropertyChanged("ReservedTables");
            }
        }

        public AdminTablePageViewModel()
        {

        }


        public void Update()
        {
            ConfigController config = ConfigController.getInstance();
            List<Classes.Models.BDModels.Table>? tables;

            int? freeid;
            int? reservedid;
            int? busyid;

            List<Classes.Models.BDModels.Table> freelist = new List<Classes.Models.BDModels.Table>();
            List<Classes.Models.BDModels.Table> reservlist = new List<Classes.Models.BDModels.Table>();

            using (AutoRestBDContext bd = new AutoRestBDContext(config.ConOptions))
            {
                tables = bd.Tables?.ToList();
                freeid = bd.Table_statuses?.Where(u => u.Title == config.TableStatusFree).FirstOrDefault()?.Id;
                reservedid = bd.Table_statuses?.Where(u => u.Title == config.TableStatusReserved).FirstOrDefault()?.Id;
                busyid = bd.Table_statuses?.Where(u => u.Title == config.TableStatusBusy).FirstOrDefault()?.Id;
            }

            if (tables == null || freeid == null || reservedid == null || busyid == null) return;

            foreach (Classes.Models.BDModels.Table table in tables)
            {
                if (table.Seats >= Seats)
                {
                    if (table.Table_StatusID == freeid)
                    {
                        freelist.Add(table);

                    }
                    else
                    {
                        reservlist.Add(table);
                    }
                }
            }

            FreeTables = new ObservableCollection<Classes.Models.BDModels.Table>(freelist);
            ReservedTables = new ObservableCollection<Classes.Models.BDModels.Table>(reservlist);


        }


        public string PersPos
        {
            get { return perspos; }
            set
            {
                perspos = value;
                OnPropertyChanged("PersPos");
            }
        }

        public string PersName
        {
            get { return persname; }
            set
            {
                persname = value;
                OnPropertyChanged("PersName");
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
