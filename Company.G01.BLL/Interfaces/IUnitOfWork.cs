using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.G01.BLL.Repositories;

namespace Company.G01.BLL.Interfaces
{

    //Unit Of Work Dssign Battern 
    public interface IUnitOfWork : IDisposable
    {
        public IDepartmentRepository DepartmentRepository { get; }
        public IEmployeeRepository EmployeeRepository { get;  }


         int Complete();  //signiture of method to implement save changes
    }
}
