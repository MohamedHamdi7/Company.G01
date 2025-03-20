using System.ComponentModel.DataAnnotations;

namespace Company.G01.PL.Models
{
    public class CreateDepartmentdto
    {
        [Required (ErrorMessage ="Code is required !")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Nameis required !")]

        public string Name { get; set; }

        [Required(ErrorMessage = "CreateAt is required !")]
        public DateTime CreateAt { get; set; }
    }
}
