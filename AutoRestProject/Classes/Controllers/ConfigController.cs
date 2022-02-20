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

        public string? OrderProcessing;
        public string? OrderWaitingPayment;
        public string? OrderDone;

        public string? OrgTitle;
        public string? ConStr;

        public string? Waiter;
        public string? Chief;
        public DbContextOptions<AutoRestBDContext> ConOptions;

        ConfigController()
        {
            OrgTitle = ConfigurationManager.AppSettings.Get("OrgTitle");
            Waiter = ConfigurationManager.AppSettings.Get("WaiterTitle");
            Waiter = ConfigurationManager.AppSettings.Get("ChiefTitle");

            OrderProcessing = ConfigurationManager.AppSettings.Get("OrderProcessing");
            OrderWaitingPayment = ConfigurationManager.AppSettings.Get("OrderWaitingPayment");
            OrderDone = ConfigurationManager.AppSettings.Get("OrderDone");

            ConStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            ConOptions = new DbContextOptionsBuilder<AutoRestBDContext>().UseSqlServer(ConStr).Options;
        }
    }
}
