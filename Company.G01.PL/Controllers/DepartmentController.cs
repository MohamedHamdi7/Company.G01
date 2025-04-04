using AutoMapper;
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
        private readonly IMapper mapper;

        //Ask clr to create object fromDepartmentRepository From Call him from ctor and use Services(Scoppe)
        public DepartmentController(IDepartmentRepository _repository,IMapper _mapper)
        {
            repository = _repository; //ctor
            mapper = _mapper;
        }

        [HttpGet] //Get //Cotroller
        public IActionResult Index(string? SearchInput)
        {
            //DepartmentRepository departmentRepository = new DepartmentRepository();
            IEnumerable<Department> department;
            if (string.IsNullOrEmpty(SearchInput))
            {
                department = repository.GetAll();
            }
            else
            {
                department = repository.GetByName(SearchInput);
                
            }
            
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
                //manual mapping
                //var deparetment = new Department()
                //{
                //    Code = model.Code,
                //    Name = model.Name,
                //    CreateAt = model.CreateAt
                //};

                var department = mapper.Map<Department>(model);   //AutoMapping using AutoMapper
                var count=repository.Add(department);
                if(count>0)
                {
                    TempData["Message"] = "Department is created";  //Dictionary
                    return RedirectToAction(nameof(Index));   
                }

           }


            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id , string viewName="Details")
        {
            if (id is null) return BadRequest("invalid id");
            var department=repository.Get(id.Value);
            if(department is null) return NotFound(new {stutascode=404,message=$"department with{id}is not found"});
            //var dto = mapper.Map<CreateDepartmentdto>(department);

            return View(viewName, department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest("invalid id");
            var department = repository.Get(id.Value);
            if (department is null) return NotFound();
            var dto = mapper.Map<CreateDepartmentdto>(department);
            return View(dto);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (ModelState.IsValid)
            {
                //if(id != department.Id) return BadRequest();
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

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //if (id is null) return BadRequest("invalid id");
            //var department = repository.Get(id.Value);
            //if (department is null) return NotFound(new { stutascode = 404, message = $"department with{id}is not found" });
            return Details(id,"Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int id,Department department)
        {
            if (ModelState.IsValid)
            {
                if (id == department.Id)
                {
                    var result = repository.Delete(department);
                    if(result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(department);
        }









    }




}
