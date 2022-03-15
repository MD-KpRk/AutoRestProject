using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoRestProject.ViewModels
{
    class AddOrderPage1ViewModel : INotifyPropertyChanged
    {
        string currPersName = "", currPersPos = "";
        int seats = 1;
        ObservableCollection<Classes.Models.BDModels.Table> tables = new ObservableCollection<Classes.Models.BDModels.Table>();
        ObservableCollection<Classes.Models.BDModels.Table> sortedtables = new ObservableCollection<Classes.Models.BDModels.Table>();
        public ObservableCollection<Classes.Models.BDModels.Table> Tables
        {
            get { return tables; }
            set
            {
                tables = value;
                OnPropertyChanged("Tables");
            }
        }
        public ObservableCollection<Classes.Models.BDModels.Table> SortedTables
        {
            get { return sortedtables; }
            set
            {
                sortedtables = value;
                OnPropertyChanged("SortedTables");
            }
        }
        public int Seats
        {
            get { return seats; }
            set
            {
                seats = value;
                OnPropertyChanged("Seats");
            }
        }
        public void Sub()
        {
            if (Seats - 1 > 0)
            {
                Seats--;
            }
        }

        public void Add()
        {
            Seats++;
        }
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

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
