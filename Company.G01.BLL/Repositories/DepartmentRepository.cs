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
    public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository
    {



        //private readonly CompanyDbContext context; //Null

        // Ask Clr To Call Object from CompanyDbContext 
        public DepartmentRepository(CompanyDbContext _context) :base(_context)
        {
            //context = _context;
        }

        //public IEnumerable<Department> GetAll()
        //{
        //   return context.Departments.ToList();
        //}

        //public Department Get(int id)
        //{
        //    return context.Departments.Find(id);
        //}

        //public int Add(Department model)
        //{
        //    context.Departments.Add(model);
        //    return context.SaveChanges();
        //}

        //public int Update(Department model)
        //{
        //    context.Departments.Update(model);
        //    return context.SaveChanges();
        //}

        //public int Delete(Department model)
        //{
        //    context.Departments.Remove(model);
        //    return context.SaveChanges();
        //}






    }
}
