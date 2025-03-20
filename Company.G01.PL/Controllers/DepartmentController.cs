using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G01.DAL.Models;
using Company.G01.PL.Dtos;
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
        [ValidateAntiForgeryToken]
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

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null) return BadRequest("invalid id");
            var department=repository.Get(id.Value);
            if(department is null) return NotFound(new {stutascode=404,message=$"department with{id}is not found"});
            return View(department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest("invalid id");
            var department=repository.Get(id.Value);
            if (department is null) return NotFound();
            return View(department);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                //if(id == department.Id) return BadRequest();
                //or
                if (id == department.Id)
                {
                    var result = repository.Update(department);
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            return View(department);
        }

        // or create UpdateDepartmentDto And use it As a parameter

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute]int id,UpdateDepartmentDto model)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        var department = new Department() { 
        //            Id=id,
        //            Name=model.Name,
        //            Code=model.Code,
        //            CreateAt=model.CreateAt
        //        };
        //        var count=repository.Update(department);
        //        if(count>0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }

        //    }
        //    return View(model);
        //}












    }




}
