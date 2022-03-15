using AutoRestProject.Classes.Models.BDModels;
using AutoRestProject.Classes.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;

namespace AutoRestProject.Resources.Pages
{
    public partial class StatPage : Page
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
            Microsoft.Office.Interop.Excel.Application? excelApplication = null;

            try
            {
                excelApplication = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook excelWorkBook = excelApplication.Workbooks.Add();
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkBook.Worksheets[1];

                worksheet.Cells[1, 1] = "ФИО сотрудника";
                worksheet.Cells[1, 2] = "Должность сотрудника";
                worksheet.Cells[1, 3] = "Телефон сотрудника";
                worksheet.Cells[1, 4] = "Количество сделаных блюд за смену";

                using (AutoRestBDContext bd = new AutoRestBDContext(ConfigController.getInstance().ConOptions))
                {
                    for (int i = 0, j = 2; i < ViewModel.Povars.Count; i++)
                    {
                        Personal? pers = bd.Personals?.Where(u => u.id == ViewModel.Povars[i].Id).Include(u => u.Position).FirstOrDefault();
                        if (pers != null)
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
                string str = @"Povar_" + string.Format("{0:dd_MM_yyyy_HH_mm_ss}", DateTime.Now) + ".xls";
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
        void CreateWaiterReport()
        {
            Microsoft.Office.Interop.Excel.Application? excelApplication = null;

            try
            {
                excelApplication = new
                    Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook excelWorkBook = excelApplication.Workbooks.Add();
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkBook.Worksheets[1];
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
                string str = @"Waiter_" + string.Format("{0:dd_MM_yyyy_HH_mm_ss}", DateTime.Now) + ".xls";
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
