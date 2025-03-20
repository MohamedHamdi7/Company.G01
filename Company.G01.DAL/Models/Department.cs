using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G01.DAL.Models
{
    public class Department
    {
        [Required(ErrorMessage ="not found")]
        public int Id { get; set; }
        public string Code { get; set; }

        [Required(ErrorMessage ="name is required !")]
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
