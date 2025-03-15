using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G01.DAL.Models;
using Company.G01.PL.Models;
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

        [HttpGet] //Get //Cotroller
        public IActionResult Index()
        {
            //DepartmentRepository departmentRepository = new DepartmentRepository();
            var department=repository.GetAll();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentdto model)
        {
           if(ModelState.IsValid)
           {
                var deparetment = new Department()
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };
                var count=repository.Add(deparetment);
                if(count>0)
                {
                    return RedirectToAction(nameof(Index));   
                }

           }


            return View(model);
        }
    }
}
