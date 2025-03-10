﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Company.G01.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.G01.DAL.Data.Contexts
{
     public class CompanyDbContext : DbContext
    {
        public CompanyDbContext() :base() 
        { 
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.; Database = CompanyG01; Trusted_Connection = True; TrustServerCertificate =True");
        }

        public DbSet<Department> Departments { get; set; }
    }

 
}
