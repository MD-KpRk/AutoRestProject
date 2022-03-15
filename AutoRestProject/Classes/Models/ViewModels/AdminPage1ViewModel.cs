using System.ComponentModel;
using System.Runtime.CompilerServices;

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
