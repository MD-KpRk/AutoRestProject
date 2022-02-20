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
        public ObservableCollection<Menu_string> Menu_Strings { get; set; } = new ObservableCollection<Menu_string>();
        public ObservableCollection<Food> Foods { get; set; } = new ObservableCollection<Food>();

        public string currPersName = "", currPersPos = "";

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
            UpdateContent();
        }

        public void UpdateContent()
        {
            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                List<Menu_string>? menu_strings = bd.Menu_strings?.Include(u => u.Food).ToList();
                if (menu_strings != null)
                    Menu_Strings = new ObservableCollection<Menu_string>(menu_strings);
                else
                    ErrorBox.getInstance().Show("Меню не загружено либо отсуствует");

                List<Food>? foods = bd.Foods?.ToList();

                if (foods != null)
                    Foods = new ObservableCollection<Food>(foods);
            }
        }



        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
