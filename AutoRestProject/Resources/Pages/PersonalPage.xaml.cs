using AutoRestProject.Classes.Enums;
using AutoRestProject.Classes.Models.BDModels;
using AutoRestProject.Classes.Models.ViewModels;
using AutoRestProject.Resources.UserControls;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoRestProject.Resources.Pages
{
    /// <summary>
    /// Логика взаимодействия для PersonalPage.xaml
    /// </summary>
    public partial class PersonalPage : Page
    {
        EnumState State { get; set; } = EnumState.ADD;
        Personal Emp;
        Personal? EditEmp;
        PersonalPageViewModel ViewModel = new PersonalPageViewModel();
        public bool CanScroll { get; set; } = true;

        public PersonalPage(Personal Emp)
        {
            ViewModel.PersPos = Emp.Position.Title;
            ViewModel.PersName = Emp.First_name + " " + Emp.Second_name + " " + Emp.Patronymic;
            DataContext = ViewModel;
            this.Emp = Emp;

            
            UpdatePos();
            InitializeComponent();
            Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // назад
        {
            PageController.getInstance()?.Goto(new AdminPage1(Emp));
        }

        public void UpdatePos()
        {
            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                List<Position>? positions = bd.Positions?.ToList();
                if (positions == null) return;
                ViewModel.Positions = new ObservableCollection<Position>(positions);
                ViewModel.SelectedPos = positions[0];
            }
        }


        public void Update()
        {
            stack.Children.Clear();
            using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
            {
                List<Personal>? personals = bd.Personals?.Include(u => u.Position).ToList();
                if (personals == null) return;
                foreach (Personal personal in personals)
                {
                    stack.Children.Add(new PersonalUserControl(this, personal));
                }
            }
        }



        Point scrollMousePoint = new Point();
        double hOff = 1;
        private void scrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            scrollMousePoint = e.GetPosition(scrollviewer);
            hOff = scrollviewer.VerticalOffset;
            if (CanScroll == true)
                scrollviewer.CaptureMouse();
        }
        private void scrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (scrollviewer.IsMouseCaptured)
                scrollviewer.ScrollToVerticalOffset(hOff + (scrollMousePoint.Y - e.GetPosition(scrollviewer).Y));
        }
        private void scrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollviewer.ReleaseMouseCapture();
        }

        private void scrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollviewer.ScrollToHorizontalOffset(scrollviewer.VerticalOffset + e.Delta);
        }

        public void SetEditMode(Personal pers)
        {
            ViewModel.ClearPanel();
            State = EnumState.EDIT;
            EditEmp = pers;

            ViewModel.ComboName = pers.First_name;
            ViewModel.ComboSecName = pers.Second_name;
            ViewModel.ComboPatr = pers.Patronymic;
            ViewModel.ComboTel = pers.Phone_number;
            ViewModel.ComboPIN = pers.Pin.ToString();
            ViewModel.SelectedPos = ViewModel.Positions.Where(u => u.Id == pers.PositionId).FirstOrDefault();

            ViewModel.PanelMode = "Изменение сотрудника";
            ViewModel.PanelVisible = Visibility.Visible;
            
        }

        public void SetAddMode()
        {
            State = EnumState.ADD;
            ViewModel.ClearPanel();
            ViewModel.PanelMode = "Добавление сотрудника";
            ViewModel.PanelVisible = Visibility.Collapsed;
        }


        private void Button_Click_1(object sender, RoutedEventArgs e) // add
        {
            string Name = ViewModel.ComboName;
            string SecName = ViewModel.ComboSecName;
            string Patr = ViewModel.ComboPatr;
            string Tel = ViewModel.ComboTel;
            string PIN = ViewModel.ComboPIN;
            Position? pos = ViewModel.SelectedPos;
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(SecName) || string.IsNullOrEmpty(Tel) || string.IsNullOrEmpty(PIN) || pos == null)
            {
                ErrorBox.getInstance().Show("Не все обязательные поля заполнены");
                return;
            }

            int intPIN;
            if(!int.TryParse(PIN,out intPIN))
            {
                ErrorBox.getInstance().Show("PIN может содержать только цифры");
                return;
            }

            if(!Regex.IsMatch(Tel, @"^(\+)(\d){10,14}", RegexOptions.IgnoreCase))
            {
                ErrorBox.getInstance().Show("Неправильный формат номера мобильного телефона. Пример номера +375291051243");
                return;
            }

            if (EditEmp == null)
            {
                ErrorBox.getInstance().Show("Ошибка. Редактируемый пользователь не определён");
                return;
            }

            try
            {
                using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
                {
                    List<Personal>? checklist = bd.Personals?.Where(u => u.Pin == intPIN).ToList();

                    if(State == EnumState.ADD)
                    {
                        if(checklist == null) return;
                        if (checklist?.Count > 0)
                        {
                            ErrorBox.getInstance().Show("Пользователь с таким PIN уже существует");
                            return;
                        }
                    }

                    if (State == EnumState.EDIT)
                    {
                        if(checklist == null) return;
                        for(int i=0; i<checklist.Count;i++)
                        {
                            if(checklist[i].id != EditEmp.id)
                            {
                                ErrorBox.getInstance().Show("PIN "+ intPIN +" занят другим сотрудником");
                                return;
                            }
                        }
                    }

                    if (State == EnumState.ADD)
                        bd.Personals?.Add(new Personal() { First_name = Name, Second_name = SecName, Patronymic = Patr, Phone_number = Tel, Pin = intPIN, PositionId = pos.Id });
                    else
                    {
                        Personal? contextPers = bd.Personals?.Where(u => u.id == EditEmp.id)?.FirstOrDefault();
                        if (contextPers == null) return;

                        contextPers.First_name = Name;
                        contextPers.Second_name = SecName;
                        contextPers.Patronymic = Patr;
                        contextPers.Pin = intPIN;
                        contextPers.PositionId = pos.Id;
                        contextPers.Phone_number = Tel;
                    }
                    
                    bd.SaveChanges();
                    ViewModel.ClearPanel();
                    Update();
                    SetAddMode();
                
                }
            }
            catch (Exception)
            {
                ErrorBox.getInstance().Show("Ошибка добавления");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) 
        {
            SetAddMode();
        }
    }
}
