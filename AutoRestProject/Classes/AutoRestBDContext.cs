﻿using AutoRestProject.Classes.Models.BDModels;
using Microsoft.EntityFrameworkCore;

namespace AutoRestProject
{
    public class AutoRestBDContext : DbContext
    {
        public AutoRestBDContext(DbContextOptions<AutoRestBDContext> options)
            : base(options)
        {
        }
        public DbSet<Food>? Foods { get; set; }
        public DbSet<Menu_string>? Menu_strings { get; set; }
        public DbSet<Order_string_status>? Order_string_statuses { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Order_status>? Order_statuses { get; set; }
        public DbSet<Order_string>? Order_strings { get; set; }
        public DbSet<Personal>? Personals { get; set; }
        public DbSet<Position>? Positions { get; set; }
        public DbSet<Table>? Tables { get; set; }
        public DbSet<Table_status>? Table_statuses { get; set; }
    }
}
