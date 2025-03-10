using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    public class DepartmentController : Controller
    {
       private readonly IDepartmentRepository repository;

        //Ask clr to create object fromDepartmentRepository From Call him from ctor and use Services(Scoppe)
        public DepartmentController(IDepartmentRepository _repository)
        {
            repository = _repository;
        }

        public IActionResult Index()
        {
            //DepartmentRepository departmentRepository = new DepartmentRepository();
            var department=repository.GetAll();
            return View(department);
        }
    }
}
