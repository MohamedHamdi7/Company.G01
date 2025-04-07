using System;
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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if(typeof(T)==typeof(Employee))
            {
                return  (IEnumerable<T>)await context.Employees.Include(E=>E.Department).ToListAsync();
            }
           return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return await context.Employees.Include(E => E.Department).FirstOrDefaultAsync(E=>E.Id==id) as T;
            }
            return await context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T model)
        {
           await context.Set<T>().AddAsync(model);
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
