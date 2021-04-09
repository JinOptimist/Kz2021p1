using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public abstract class PersonViewModel
    {
        public long Id { get; set; }
        public string IIN { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Birthday { get; set; }
        public string Email { get; set; }
    }
}
