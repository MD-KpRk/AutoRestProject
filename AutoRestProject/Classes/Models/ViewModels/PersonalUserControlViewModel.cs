using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AutoRestProject.Classes.Models.ViewModels
{
    class PersonalUserControlViewModel : INotifyPropertyChanged
    {
        string persPos = "", persPIN = "", persName = "", persTel = "";

        public string PersPos
        {
            get { return persPos; }
            set
            {
                persPos = value;
                OnPropertyChanged("PersPos");
            }
        }

        public string PersPIN
        {
            get { return persPIN; }
            set
            {
                persPIN = value;
                OnPropertyChanged("PersPIN");
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

        public string PersTel
        {
            get { return persTel; }
            set
            {
                persTel = value;
                OnPropertyChanged("PersTel");
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
