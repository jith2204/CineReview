using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class RegisterModel
    {
        [Required] 
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[^@\\s]+@[^@\\s]+\\.(com|net|org|gov)$")]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [RegularExpression(@"((?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*\W).{6,})", ErrorMessage = "Password complexity requirements are minimum 6 alphanumeric characters with at least 1 number, 1 capital letter, 1 small letter and 1 special character !@#$%^*.()")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
