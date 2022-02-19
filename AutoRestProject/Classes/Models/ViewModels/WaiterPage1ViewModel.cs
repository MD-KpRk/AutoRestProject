using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoRestProject.ViewModels
{
    public class WaiterPage1ViewModel : INotifyPropertyChanged
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

        public void SetPerson(string name, string position)
        {
            PersName = name;
            PersPos = position;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
