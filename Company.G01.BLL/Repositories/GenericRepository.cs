﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Data.Contexts;
using Company.G01.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.G01.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext context;
        public GenericRepository(CompanyDbContext _context)
        {
            context= _context;

        }

        public IEnumerable<T> GetAll()
        {
            if(typeof(T)==typeof(Employee))
            {
                return (IEnumerable<T>) context.Employees.Include(E=>E.Department).ToList();
            }
           return context.Set<T>().ToList();
        }

        public T? Get(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return context.Employees.Include(E => E.Department).FirstOrDefault(E=>E.Id==id) as T;
            }
            return context.Set<T>().Find(id);
        }

        public void Add(T model)
        {
            context.Set<T>().Add(model);
            //return context.SaveChanges();

        }
        public void Update(T model)
        {
            context.Set<T>().Update(model);
            //return context.SaveChanges();
        }
        public void Delete(T model)
        {
            context.Set<T>().Remove(model);
            //return context.SaveChanges();
        }

        

       

        
    }
}
