using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class Pupil : Person
    {
        public int? ClassYear { get; set; }
        public int AverageMark { get; set; }
        public DateTime? GraduatedYear { get; set; }
        public int? ENT { get; set; }
        public virtual School School { get; set; }
        public virtual Certificate Certificate { get; set; }
    }
}
