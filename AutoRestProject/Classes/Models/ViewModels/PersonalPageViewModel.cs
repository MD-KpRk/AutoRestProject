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
    class PersonalPageViewModel : INotifyPropertyChanged
    {
        string persName = "", persPos = "";
        Position? selectedPos = null;

        ObservableCollection<Position> positions = new ObservableCollection<Position>();

        public ObservableCollection<Position> Positions
        {
            get { return positions; }
            set
            {
                positions = value;
                OnPropertyChanged("Positions");
            }
        }

        public Position? SelectedPos
        {
            get { return selectedPos; }
            set
            {
                selectedPos = value;
                OnPropertyChanged("SelectedPos");
            }
        }
            


        public string PersName
        {
            get { return persName; }
            set
            {
                persName = value;
                OnPropertyChanged("PersName");
            }
        }

        public string PersPos
        {
            get { return persPos; }
            set
            {
                persPos = value;
                OnPropertyChanged("PersPos");
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
