using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class StudentViewModel : PersonViewModel
    {
        public string Faculty { get; set; }
        public int CourseYear { get; set; }
        public double Gpa { get; set; }
        public bool OnGrant { get; set; }
        public DateTime EnteredYear { get; set; }
        public DateTime? GraduatedYear { get; set; }
        public long? UniversityId { get; set; }
        public UniversityViewModel University { get; set; }
    }
}
