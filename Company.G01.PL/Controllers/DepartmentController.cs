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

        //private readonly IDepartmentRepository repository;

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        //Ask clr to create object fromDepartmentRepository From Call him from ctor and use Services(Scoppe)
        public DepartmentController
            (

            //IDepartmentRepository _repository, 

            IUnitOfWork _unitOfWork,
            IMapper _mapper

            )
        {
            //repository = _repository; //ctor
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        [HttpGet] //Get //Cotroller
        public IActionResult Index(string? SearchInput)
        {
            IEnumerable<Department> department;

            if (string.IsNullOrEmpty(SearchInput))
            {
                department = unitOfWork.DepartmentRepository.GetAll();
            }
            else
            {
                department = unitOfWork.DepartmentRepository.GetByName(SearchInput);
                
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
                #region manual mapping
                //manual mapping
                //var deparetment = new Department()
                //{
                //    Code = model.Code,
                //    Name = model.Name,
                //    CreateAt = model.CreateAt
                //}; 
                #endregion

                var department = mapper.Map<Department>(model);   //AutoMapping using AutoMapper
                unitOfWork.DepartmentRepository.Add(department);
                var result = unitOfWork.Complete();

                if (result>0)
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
            var department=unitOfWork.DepartmentRepository.Get(id.Value);
            if(department is null) return NotFound(new {stutascode=404,message=$"department with{id}is not found"});
            var dto = mapper.Map<CreateDepartmentdto>(department);

            return View(viewName, dto);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest("invalid id");
            var department = unitOfWork.DepartmentRepository.Get(id.Value);
            if (department is null) return NotFound();
            var dto = mapper.Map<CreateDepartmentdto>(department);
            return View(dto);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, CreateDepartmentdto department)
        {
            if (ModelState.IsValid)
            {
                //if(id != department.Id) return BadRequest();
                //or
                var dto=mapper.Map<Department>(department);  //Auto Mapper
                 dto.Id = id;
                
                     unitOfWork.DepartmentRepository.Update(dto);
                    var result = unitOfWork.Complete();

                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
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
        public IActionResult Delete([FromRoute]int id,CreateDepartmentdto department)
        {
            if (ModelState.IsValid)
            {
               var dto= mapper.Map<Department>(department);
                dto.Id=id;
                
                   unitOfWork.DepartmentRepository.Delete(dto);
                    var result = unitOfWork.Complete();
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                
            }
            return View(department);
        }









    }




}
