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

namespace AutoRestProject.ViewModels
{
    public class ChiefPage1ViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Menu_string> menu_Strings = new ObservableCollection<Menu_string>();
        public ObservableCollection<Food> foods = new ObservableCollection<Food>();
        public string currPersName = "", currPersPos = "";


        public ObservableCollection<Menu_string> Menu_Strings
        {
            get { return menu_Strings; }
            set
            {
                menu_Strings = value;
                OnPropertyChanged("Menu_Strings");
            }
        }

        public ObservableCollection<Food> Foods
        {
            get { return foods; }
            set
            {
                foods = value;
                OnPropertyChanged("Foods");
            }
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

        public ChiefPage1ViewModel()
        {
            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                UpdateFoods();
                UpdateMenuStrings();
            }
        }

        public void UpdateMenuStrings()
        {
            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                List<Menu_string>? menu_strings = bd.Menu_strings?.Include(u => u.Food).ToList();
                if (menu_strings != null) Menu_Strings = new ObservableCollection<Menu_string>(menu_strings);
            }
        }

        public void UpdateFoods()
        {
            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                List<Food>? foods = bd.Foods?.ToList();
                if (foods != null) Foods = new ObservableCollection<Food>(foods);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
