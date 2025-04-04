using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G01.DAL.Data.Contexts;

namespace Company.G01.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyDbContext context;

        public IDepartmentRepository DepartmentRepository { get; } //------> Null

        public IEmployeeRepository EmployeeRepository { get; }     //------> Null


        // call ctor if exist (2 object from emp and dep )
        public UnitOfWork(CompanyDbContext _context)
        {
            context = _context;
            DepartmentRepository =new DepartmentRepository(context);
            EmployeeRepository=new  EmployeeRepository(context);
            
        }



        // method to make one only save change
        public int Complete()
        {
          return  context.SaveChanges();
        }

        public void Dispose()
        {
           context.Dispose();
        }
    }
}
