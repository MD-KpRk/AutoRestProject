﻿using System;
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

        public string? OrgTitle;
        public string? ConStr;


        ConfigController()
        {
            OrgTitle = ConfigurationManager.AppSettings.Get("OrgTitle");
            ConStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            /*
            string connectionString =
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            if (sAttr != null)
                MessageBox.Show(sAttr);
            MessageBox.Show(connectionString);
            */
        }
    }
}