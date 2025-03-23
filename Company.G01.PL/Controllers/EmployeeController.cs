using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G01.DAL.Models;
using Company.G01.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository repository;

        //ctor
        public EmployeeController(IEmployeeRepository _repository)
        {
            repository = _repository;
        }



        [HttpGet]
        public IActionResult Index()
        {
            var employee = repository.GetAll();
            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Name = model.Name,
                    Address = model.Address,
                    Age = model.Age,
                    Phone = model.Phone,
                    Email = model.Email,
                    CreateAt = model.CreateAt,
                    HiringDate = model.HiringDate,
                    Salary = model.Salary,
                    IsActive = model.IsActive,
                    IsDelete = model.IsDelete,

                };
                var count=repository.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int?id,string ViewName="Details")
        {
            if (id is null) return BadRequest("invalid id");
            var employee = repository.Get(id.Value);
            if (employee == null) return NotFound(new {Statuscode=404 , message=$"Employee with id{id}is not found"});
            return View(ViewName,employee);
        }

        [HttpGet]
        public IActionResult Edit(int?id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id,Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (id == employee.Id)
                {
                    var count = repository.Update(employee);

                    if (count > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(employee);
        }


        [HttpGet]
        public IActionResult Delete(int?id)
        {
            return Details(id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int? id,Employee employee)
        {
          if(ModelState.IsValid)
          {
                if (id == employee.Id)
                {
                    var count = repository.Delete(employee);
                    if(count > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
                
          }
            return View(employee); 
        }
    }
}
