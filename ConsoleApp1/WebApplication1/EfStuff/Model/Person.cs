using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class Person : BaseModel
    {
        [Required]
        [MaxLength(12)]
        public string IIN { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public string Surname { get; set; }

        [MaxLength(30)]
        public string Patronymic { get; set; }
        public string Birthday { get; set; }
        [Required]
        [MaxLength(30)]
        public string Email { get; set; }
    }
}
