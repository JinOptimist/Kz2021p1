using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class Pupil : Person
    {
        [MaxLength(50)]
        public string Subject { get; set; }
        public int ClassYear { get; set; }
        public double AverageMark { get; set; }
        public DateTime? GraduatedYear { get; set; }
        public int? ENT { get; set; }
        public long SchoolId { get; set; } // FK
        public virtual School School { get; set; }  // навигационное свойство

        public virtual ICollection<Certificate> Certificates { get; set; } // = new List<Course>();

    }
}
