using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Company.G01.PL.Dtos
{
    public class CreateEmployeeDto
    {

        [Required(ErrorMessage = "Name is requird")]
        public string Name { get; set; }

        [Range(22,60,ErrorMessage ="age must be between(22,60)")]
        public int? Age { get; set; }
        [DataType(DataType.EmailAddress,ErrorMessage ="Email is not valid")]
        public string Email { get; set; }

        //[RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",ErrorMessage ="Address must be like 123-street-city-country")]
        public string Address { get; set; }
        //[Phone]
        public int Phone { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

        [DisplayName("Hiring Name")]
        public DateTime HiringDate { get; set; }

        [DisplayName("Date Of Creation")]

        public DateTime CreateAt { get; set; }

    }
}
