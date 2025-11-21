using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WPF_Starter.Models;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace WPF_Starter.DataBase
{
    public class AppDbContext : DbContext
    {
        public DbSet<People> Person { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            string connectionString = "Server=NINJA2077\\MSSQLCSV;Database=Person;Trusted_Connection=True;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
