using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AutoRestProject.Classes.Models.BDModels;

namespace AutoRestProject
{
    public class AutoRestBDContext : DbContext
    {
        public AutoRestBDContext(DbContextOptions<AutoRestBDContext> options )
            : base(options)
        {
        }

        //public DbSet<Check>? Checks { get; set; }
        public DbSet<Food>? Foods { get; set; }
        //public DbSet<Log_string>? Log_strings { get; set; }
        //public DbSet<Menu_string>? Menu_strings { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Order_status>? Order_statuses { get; set; }
        public DbSet<Order_string>? Order_strings { get; set; }
        public DbSet<Personal>? Personals { get; set; }
        public DbSet<Position>? Positions { get; set; }
        public DbSet<Table>? Tables { get; set; }
        public DbSet<Table_status>? Table_statuses { get; set; }
    }
}