using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoRestProject
{
    class ConfigController
    {
        private static ConfigController? instance;
        public static ConfigController getInstance()
        {
            if (instance == null)
                instance = new ConfigController();
            return instance;
        }

        public int? GoodTimeSecond;  
        public int? AverageTimeSecond;  
        public int? BadTimeSecond;  



        public string? OrderProcessing;
        public string? OrderWaitingPayment;
        public string? OrderDone;


        public string? OrderStringDone;
        public string? OrderStringProcessing;
        public string? OrderStringNotDone;

        public string? TableStatusFree;
        public string? TableStatusReserved;
        public string? TableStatusBusy;

        public string? OrgTitle;
        public string? ConStr;

        public string? Waiter;
        public string? Admin;
        public string? Chief;
        public DbContextOptions<AutoRestBDContext> ConOptions;

        ConfigController()
        {
            OrgTitle = ConfigurationManager.AppSettings.Get("OrgTitle");

            Waiter = ConfigurationManager.AppSettings.Get("WaiterTitle");
            Chief = ConfigurationManager.AppSettings.Get("ChiefTitle");
            Admin = ConfigurationManager.AppSettings.Get("Admin");

            TableStatusFree = ConfigurationManager.AppSettings.Get("TableStatusFree");
            TableStatusReserved = ConfigurationManager.AppSettings.Get("TableStatusReserved");
            TableStatusBusy = ConfigurationManager.AppSettings.Get("TableStatusBusy");

            GoodTimeSecond = Convert.ToInt32(ConfigurationManager.AppSettings.Get("GoodTimeSecond"));
            AverageTimeSecond = Convert.ToInt32(ConfigurationManager.AppSettings.Get("AverageTimeSecond"));
            BadTimeSecond = Convert.ToInt32(ConfigurationManager.AppSettings.Get("BadTimeSecond"));


            OrderStringDone = ConfigurationManager.AppSettings.Get("OrderStringDone");
            OrderStringProcessing = ConfigurationManager.AppSettings.Get("OrderStringProcessing");
            OrderStringNotDone = ConfigurationManager.AppSettings.Get("OrderStringNotDone");

            OrderProcessing = ConfigurationManager.AppSettings.Get("OrderProcessing");
            OrderWaitingPayment = ConfigurationManager.AppSettings.Get("OrderWaitingPayment");
            OrderDone = ConfigurationManager.AppSettings.Get("OrderDone");

            ConStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            ConOptions = new DbContextOptionsBuilder<AutoRestBDContext>().UseSqlServer(ConStr).Options;
        }
    }
}
