using AutoRestProject.Classes.Enums;
using AutoRestProject.Classes.Models.BDModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AutoRestProject.ViewModels
{
    public class ChiefPage1ViewModel : INotifyPropertyChanged
    {

        public PanelMode mode;

        public ObservableCollection<Menu_string> menu_Strings = new ObservableCollection<Menu_string>();
        public ObservableCollection<Food> foods = new ObservableCollection<Food>();
        string currPersName = "", currPersPos = "";
        string panelFoodTitle = "";
        string panelButtonMode = "";
        double panelFoodPrice;
        bool panelFoodCook = false;

        Visibility menuVisible = Visibility.Collapsed;
        Visibility panelVisible = Visibility.Collapsed;

        public void FoodPanelEdit(Food food)
        {
            PanelFoodTitle = food.Title;
            PanelFoodPrice = food.Price;
            PanelFoodCook = food.Is_cooking;
            mode = PanelMode.EDIT;
            PanelVisible = Visibility.Visible;
        }

        public void FoodPanelAdd()
        {
            PanelFoodTitle = "";
            PanelFoodPrice = 1;
            PanelFoodCook = false;
            mode = PanelMode.ADD;
            PanelVisible = Visibility.Visible;
        }

        public string PanelButtonMode
        {
            get { return panelButtonMode; }
            set
            {
                panelButtonMode = value;
                OnPropertyChanged("PanelButtonMode");
            }
        }

        public string PanelFoodTitle
        {
            get { return panelFoodTitle; }
            set
            {
                panelFoodTitle = value;
                OnPropertyChanged("PanelFoodTitle");
            }
        }

        public double PanelFoodPrice
        {
            get { return panelFoodPrice; }
            set
            {
                panelFoodPrice = value;
                OnPropertyChanged("PanelFoodPrice");
            }
        }

        public bool PanelFoodCook
        {
            get { return panelFoodCook; }
            set
            {
                panelFoodCook = value;
                OnPropertyChanged("PanelFoodCook");
            }
        }

        public Visibility MenuVisible
        {
            get { return menuVisible; }
            set
            {
                menuVisible = value;
                OnPropertyChanged("MenuVisible");
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
                if (menu_strings != null)
                {
                    Menu_Strings = new ObservableCollection<Menu_string>(menu_strings);
                }
            }
        }

        public void UpdateFoods()
        {
            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                List<Food>? foods = bd.Foods?.ToList();
                if (foods != null)
                {
                    Foods = new ObservableCollection<Food>(foods);
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
