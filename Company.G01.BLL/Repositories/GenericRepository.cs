using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.G01.BLL.Interfaces;
using Company.G01.DAL.Data.Contexts;
using Company.G01.DAL.Models;

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
           return context.Set<T>().ToList();
        }

        public T? Get(int id)
        {
            return context.Set<T>().Find(id);
        }

        public int Add(T model)
        {
            context.Set<T>().Add(model);
            return context.SaveChanges();
        }
        public int Update(T model)
        {
            context.Set<T>().Update(model);
            return context.SaveChanges();
        }
        public int Delete(T model)
        {
            context.Set<T>().Remove(model);
            return context.SaveChanges();
        }

        

       

        
    }
}
