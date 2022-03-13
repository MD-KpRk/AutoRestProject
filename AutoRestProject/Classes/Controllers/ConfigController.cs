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

        public int? GoodTimeSecond, AverageTimeSecond, BadTimeSecond;  
        public string? OrderProcessing, OrderWaitingPayment, OrderDone;
        public string? OrderStringDone, OrderStringProcessing, OrderStringNotDone;
        public string? TableStatusFree, TableStatusReserved, TableStatusBusy;
        public string? OrgTitle, ConStr;
        public string? Waiter, Admin, Chief, Cook;

        public DbContextOptions<AutoRestBDContext> ConOptions;

        ConfigController()
        {
            OrgTitle = ConfigurationManager.AppSettings.Get("OrgTitle");

            Waiter = ConfigurationManager.AppSettings.Get("WaiterTitle");
            Chief = ConfigurationManager.AppSettings.Get("ChiefTitle");
            Admin = ConfigurationManager.AppSettings.Get("Admin");
            Cook = ConfigurationManager.AppSettings.Get("CookTitle");

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
