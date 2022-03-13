using AutoRestProject.Classes.Models.BDModels;
using AutoRestProject.Classes.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using Microsoft.Office.Interop.Excel;
using System.ComponentModel;
using Range = Microsoft.Office.Interop.Excel.Range;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace AutoRestProject.Resources.Pages
{
    /// <summary>
    /// Логика взаимодействия для StatPage.xaml
    /// </summary>
    public partial class StatPage : System.Windows.Controls.Page
    {
        StatPageViewModel ViewModel = new StatPageViewModel();
        Personal Emp;

        public StatPage(Personal emp)
        {
            Emp = emp;
            ViewModel.Update();
            DataContext = ViewModel;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PageController.getInstance()?.Goto(new AdminPage1(Emp));
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreatePovarReport();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CreateWaiterReport();
        }

        void CreatePovarReport()
        {
            Microsoft.Office.Interop.Excel.Application excelApplication = null;

            try
            {
                excelApplication = new
                    Microsoft.Office.Interop.Excel.Application();
                Workbook excelWorkBook = excelApplication.Workbooks.Add();
                Worksheet worksheet = (Worksheet)excelWorkBook.Worksheets[1];

                //строка, столбец

                worksheet.Cells[1, 1] = "ФИО сотрудника";
                worksheet.Cells[1, 2] = "Должность сотрудника";
                worksheet.Cells[1, 3] = "Телефон сотрудника";
                worksheet.Cells[1, 4] = "Количество сделаных блюд за смену";


                using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
                {
                    for (int i = 0, j=2; i < ViewModel.Povars.Count; i++)
                    {
                        Personal? pers = bd.Personals?.Where(u=> u.id == ViewModel.Povars[i].Id).Include(u=>u.Position).FirstOrDefault();
                        if(pers != null)
                        {
                            worksheet.Cells[j, 1] = pers.First_name + " " + pers.Second_name + " " + pers.Patronymic;
                            worksheet.Cells[j, 2] = pers.Position.Title;
                            worksheet.Cells[j, 3] = pers.Phone_number + " РБ";
                            worksheet.Cells[j, 4] = ViewModel.Povars[i].Count;

                            j++;
                        }
                    }
                }

                worksheet.Columns.AutoFit();
                string str = @"D:\" + @"Povar_" + string.Format("{0:dd_MM_yyyy_HH_mm_ss}", DateTime.Now) + ".xls";
                excelWorkBook.SaveAs(str);
                ErrorBox.getInstance().ShowInfo("Файл отчёта создан");
            }
            catch (Exception)
            {
                ErrorBox.getInstance().Show("Ошибка создания отчёта");
            }
            finally
            {
                if (excelApplication != null)
                {
                    excelApplication.Quit();
                    Marshal.FinalReleaseComObject(excelApplication);
                }
            }
        }


#warning Переделать под официантов
        void CreateWaiterReport() 
        {
            Microsoft.Office.Interop.Excel.Application excelApplication = null;

            try
            {
                excelApplication = new
                    Microsoft.Office.Interop.Excel.Application();
                Workbook excelWorkBook = excelApplication.Workbooks.Add();
                Worksheet worksheet = (Worksheet)excelWorkBook.Worksheets[1];

                //строка, столбец

                worksheet.Cells[1, 1] = "ФИО сотрудника";
                worksheet.Cells[1, 2] = "Должность сотрудника";
                worksheet.Cells[1, 3] = "Телефон сотрудника";
                worksheet.Cells[1, 4] = "Количество закрытых заказов за смену";


                using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
                {
                    for (int i = 0, j = 2; i < ViewModel.Waiters.Count; i++)
                    {
                        Personal? pers = bd.Personals?.Where(u => u.id == ViewModel.Waiters[i].Id).Include(u => u.Position).FirstOrDefault();
                        if (pers != null)
                        {
                            worksheet.Cells[j, 1] = pers.First_name + " " + pers.Second_name + " " + pers.Patronymic;
                            worksheet.Cells[j, 2] = pers.Position.Title;
                            worksheet.Cells[j, 3] = pers.Phone_number + " РБ";
                            worksheet.Cells[j, 4] = ViewModel.Waiters[i].Count;

                            j++;
                        }
                    }
                }

                worksheet.Columns.AutoFit();
                string str = @"D:\" + @"Waiter_" + string.Format("{0:dd_MM_yyyy_HH_mm_ss}", DateTime.Now) + ".xls";
                excelWorkBook.SaveAs(str);
                ErrorBox.getInstance().ShowInfo("Файл отчёта создан");
            }
            catch (Exception)
            {
                ErrorBox.getInstance().Show("Ошибка создания отчёта");
            }
            finally
            {
                if (excelApplication != null)
                {
                    excelApplication.Quit();
                    Marshal.FinalReleaseComObject(excelApplication);
                }
            }
        }

    }
}
