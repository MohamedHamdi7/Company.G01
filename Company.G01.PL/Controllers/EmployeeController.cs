﻿using AutoMapper;
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
        private readonly IDepartmentRepository departmentRepository;
        private readonly IMapper mapper;


        //ctor
        public EmployeeController
            (
            IEmployeeRepository _repository,
            IDepartmentRepository _departmentRepository,
            IMapper _mapper
            )
        {
            repository = _repository;
            departmentRepository = _departmentRepository;
            mapper = _mapper;
        }



        [HttpGet]
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchInput))
            {
                 employees = repository.GetAll();
            }
            else
            {
                 employees=repository.GetByName(SearchInput);
            }


            
            //Dictionary:3 property
            //1-ViewData :Transfer Extar E=Information From Controller(Action) To View
            //ViewData["Message"] = "welcome from viewdata";

            //2-ViewBag  :Transfer Extar E=Information From Controller(Action) To View
            //ViewBag.Message = "welcome from viewbag";

            //3-TempData : in cerate 
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var department=departmentRepository.GetAll();
            ViewData["department"]=department;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                //var employee = new Employee()
                //{
                //    Name = model.Name,
                //    Address = model.Address,
                //    Age = model.Age,
                //    Phone = model.Phone,
                //    Email = model.Email,
                //    CreateAt = model.CreateAt,
                //    HiringDate = model.HiringDate,
                //    Salary = model.Salary,
                //    IsActive = model.IsActive,
                //    IsDelete = model.IsDelete,
                //    DepartmentId=model.DepartmentId,

                //};
                var employee = mapper.Map< Employee>(model);
                var count=repository.Add(employee);
                if (count > 0)
                {
                    TempData["Message"] = "Employee is Created";  //Dictionary 
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int?id,string ViewName="Details")
        {
            var department = departmentRepository.GetAll();
            ViewData["department"] = department;
            if (id is null) return BadRequest("invalid id");
            var employee = repository.Get(id.Value);
            if (employee == null) return NotFound(new {Statuscode=404 , 
                message=$"Employee with id{id}is not found"});
            return View(ViewName,employee);
        }

        [HttpGet]
        public IActionResult Edit(int?id)
        {
            var department = departmentRepository.GetAll();
            ViewData["department"] = department;
            if (id is null) return BadRequest("invalid id");
            var employee = repository.Get(id.Value);
            if (employee == null) return NotFound(new { Statuscode = 404,
                message = $"Employee with id{id}is not found" });

             var Dto=  mapper.Map<CreateEmployeeDto>(employee);  //Automatic mapping using AutoMapper
            return View(Dto);
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
            var department=departmentRepository.GetAll();
            ViewData["department"]=department;
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
