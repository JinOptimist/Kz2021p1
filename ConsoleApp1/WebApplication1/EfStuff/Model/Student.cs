using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class Student : Person
    {
        [MaxLength(50)]
        public string Faculty { get; set; }

        public int CourseYear { get; set; }

        public double Gpa { get; set; }

        public bool OnGrant { get; set; }

        public DateTime EnteredYear { get; set; }
        public DateTime? GraduatedYear { get; set; }

        public long UniversityId { get; set; } // FK
        public virtual University University { get; set; }  // навигационное свойство

        public virtual ICollection<Certificate> Certificates { get; set; } // = new List<Course>();

    }
}
