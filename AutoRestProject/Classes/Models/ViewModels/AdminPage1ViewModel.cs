using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoRestProject.Classes.Models.ViewModels
{
    internal class AdminPage1ViewModel : INotifyPropertyChanged
    {
        string persname = "", perspos = "";
        public string PersName
        {
            get => persname;
            set
            {
                persname = value;
                OnPropertyChanged("PersName");
            }
        }

        public string PersPos
        {
            get => perspos;
            set
            {
                perspos = value;
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
