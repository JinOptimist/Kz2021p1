using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.CustomValidationAttributes;

namespace WebApplication1.Models
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(3)]
        [RequiredSymbol(" ", ErrorMessage = "Имя по формату должно быть Иван Иванов")]
        public string Name { get; set; }

        [Required]
        [RequiredSymbol("!")]
        public string Password { get; set; }
    }
}
