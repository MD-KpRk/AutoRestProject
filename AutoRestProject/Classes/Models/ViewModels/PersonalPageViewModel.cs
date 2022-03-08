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
    class PersonalPageViewModel : INotifyPropertyChanged
    {
        string persName = "", persPos = "", panelMode = "Добавление сотрудника";
        Position? selectedPos = null;

        Visibility panelVisible = Visibility.Collapsed;

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

        public string PanelMode
        {
            get { return panelMode; }
            set
            {
                panelMode = value;
                OnPropertyChanged("PanelMode");
            }
        }

        public Visibility PanelVisible
        {
            get { return panelVisible; }
            set
            {
                panelVisible = value;
                OnPropertyChanged("PanelVisible");
            }
        }

        public void ClearPanel()
        {
            ComboName = ComboPatr = ComboSecName = ComboPIN = ComboTel = "";
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

        string comboName = "", comboSecName = "", comboPatr = "", comboTel = "", comboPIN = "";
        public string ComboName
        {
            get { return comboName; }
            set { comboName = value; OnPropertyChanged("ComboName"); }
        }

        public string ComboSecName
        {
            get { return comboSecName; }
            set { comboSecName = value; OnPropertyChanged("ComboSecName"); }
        }
        public string ComboPatr
        {
            get { return comboPatr; }
            set { comboPatr = value; OnPropertyChanged("ComboPatr"); }
        }
        public string ComboTel
        {
            get { return comboTel; }
            set { comboTel = value; OnPropertyChanged("ComboTel"); }
        }
        public string ComboPIN
        {
            get { return comboPIN; }
            set { comboPIN = value; OnPropertyChanged("ComboPIN"); }
        }



        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
