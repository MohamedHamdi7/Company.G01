using AutoMapper;
using Company.G01.BLL.Interfaces;
using Company.G01.BLL.Repositories;
using Company.G01.DAL.Models;
using Company.G01.PL.Dtos;
using Company.G01.PL.Helpers;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Company.G01.PL.Controllers
{
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeRepository repository;
        //private readonly IDepartmentRepository departmentRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;


        //ctor
        public EmployeeController
            (
            //IEmployeeRepository _repository,
            //IDepartmentRepository _departmentRepository,
            IMapper _mapper,
            IUnitOfWork _unitOfWork
            )
        {
            //repository = _repository;
            //departmentRepository = _departmentRepository;
            mapper = _mapper;
            unitOfWork = _unitOfWork;
        }



        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchInput))
            {
                 //employees = repository.GetAll();
                 employees = await unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                //employees=repository.GetByName(SearchInput);
                employees =await unitOfWork.EmployeeRepository.GetByNameAsync(SearchInput);

            }
            #region Dictionary
            //Dictionary:3 property
            //1-ViewData :Transfer Extar E=Information From Controller(Action) To View
            //ViewData["Message"] = "welcome from viewdata";

            //2-ViewBag  :Transfer Extar E=Information From Controller(Action) To View
            //ViewBag.Message = "welcome from viewbag";

            //3-TempData : in cerate  
            #endregion
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var department= await unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["department"]=department;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid) //Server Side Validation
            {
                #region manual mapping
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
                #endregion

                if(model.Image is not null)
                {
                  model.ImageName=   DocumentSettings.UploadFile(model.Image, "image");
                }


                var employee = mapper.Map< Employee>(model);  //Automatic Mapping with Auto Mapper
               await  unitOfWork.EmployeeRepository.AddAsync(employee);
                var count = await unitOfWork.CompleteAsync();  //---> save changes
                if (count > 0)
                {
                    TempData["Message"] = "Employee is Created";  //Dictionary 
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id , string ViewName="Details")
        {
            //var department = unitOfWork.DepartmentRepository.GetAll();
            //ViewData["department"] = department;

            if (id is null) return BadRequest("invalid id");
            var employee =await unitOfWork.EmployeeRepository.GetAsync(id.Value);

            if (employee == null) return NotFound(new {Statuscode=404 , 
                message=$"Employee with id{id}is not found"});
            var dto=mapper.Map<CreateEmployeeDto>(employee);
            return View(ViewName,dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var department =await unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["department"] = department;

            if (id is null) return BadRequest("invalid id");
            var employee =await unitOfWork.EmployeeRepository.GetAsync(id.Value);

            if (employee == null) return NotFound(new { Statuscode = 404,
                message = $"Employee with id{id}is not found" });

             var Dto=  mapper.Map<CreateEmployeeDto>(employee);  //Automatic mapping using AutoMapper
            return View(Dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id,CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                if(model.ImageName is not null && model.Image is not null)
                {
                    DocumentSettings.DeleteFile(model.ImageName, "image");
                }
                if(model.Image is not null)
                {
                    model.ImageName= DocumentSettings.UploadFile(model.Image, "image");
                }

               

                 var employee= mapper.Map<Employee>(model);
                 employee.Id = id;
                
                   
                    unitOfWork.EmployeeRepository.Update(employee);
                    var count =await unitOfWork.CompleteAsync();

                    if (count > 0)
                    {
                        return RedirectToAction("Index");
                    }
                
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int?id)
        {
            //var department=unitOfWork.DepartmentRepository.GetAll();
            //ViewData["department"]=department;

            if (id is null) return BadRequest("invalid id");
            var employee =await unitOfWork.EmployeeRepository.GetAsync(id.Value);

            if (employee == null) return NotFound(new
            {
                Statuscode = 404,
                message = $"Employee with id {id} is not found"
            });

            var Dto = mapper.Map<CreateEmployeeDto>(employee);
            return View(Dto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]int? id,CreateEmployeeDto model)
        {
          if(ModelState.IsValid)
          {

                var dto =mapper.Map<Employee>(model);
                id = dto.Id;
                
                
                    unitOfWork.EmployeeRepository.Delete(dto);


                    var count =await unitOfWork.CompleteAsync();

                    if (count > 0)
                    {
                        if(dto.ImageName is not  null)
                        {
                           DocumentSettings.DeleteFile(model.ImageName, "image");

                        }
                          return RedirectToAction("Index");
                    }
                
                
          }
            return View(model); 
        }
    }
}
