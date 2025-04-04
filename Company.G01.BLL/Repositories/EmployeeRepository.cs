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
    public class EmployeeRepository :GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext context;
        public EmployeeRepository(CompanyDbContext _context):base(_context)
        {
            context = _context;
        }

        public List<Employee> GetByName(string name)
        {
            return context.Employees.Include(E => E.Department).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        //IEnumerable<Employee> IEmployeeRepository.GetAll()
        //{
        //    return context.Employees.ToList();
        //}

        //Employee IEmployeeRepository.Get(int id)
        //{
        //    return context.Employees.Find(id);
        //}

        //int IEmployeeRepository.Add(Employee model)
        //{
        //    context.Employees.Add(model);
        //    return context.SaveChanges();
        //}

        //int IEmployeeRepository.Update(Employee model)
        //{
        //    context.Employees.Update(model);
        //    return context.SaveChanges();
        //}


        //int IEmployeeRepository.Delete(Employee model)
        //{
        //    context.Employees.Remove(model);
        //    return context.SaveChanges();
        //}






    }
}
