using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class University : EducationalInstitution
    {
        public ICollection<Student> Students { get; set; } // навигационное свойство
    }
}
