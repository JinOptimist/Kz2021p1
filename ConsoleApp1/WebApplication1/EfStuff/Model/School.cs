using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class School : EducationalInstitution
    {
        public virtual ICollection<Pupil> Pupils { get; set; } // навигационное свойство
    }
}
