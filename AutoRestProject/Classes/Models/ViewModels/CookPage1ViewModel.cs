﻿using AutoRestProject.Classes.Models.BDModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoRestProject.Classes.Models.ViewModels
{
    public class CookPage1ViewModel : INotifyPropertyChanged
    {
        string currPersName = "", currPersPos = "";


        public void ShowPanel()
        {
            Visibility = Visibility.Visible;
        }

        public void HidePanel()
        {
            Visibility = Visibility.Collapsed;
        }


        Visibility visibility = Visibility.Collapsed;
        public Visibility Visibility
        {
            get => visibility;
            set
            {
                visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        public string CurrentPersName
        {
            get { return currPersName; }
            set
            {
                currPersName = value;
                OnPropertyChanged("CurrentPersName");
            }
        }

        public void SetPerson(Personal pers)
        {
            CurrentPersName = pers.First_name + " " + pers.Second_name;
            CurrentPersPos = pers.Position.Title;
        }

        public string CurrentPersPos
        {
            get { return currPersPos; }
            set
            {
                currPersPos = value;
                OnPropertyChanged("CurrentPersPos");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
