using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.Education;

namespace WebApplication1.Models
{
    public class StudentViewModel : PersonViewModel
    {
        public string Faculty { get; set; }
        public int CourseYear { get; set; }
        public double Gpa { get; set; }
        public bool IsGrant { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime EnteredYear { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? GraduatedYear { get; set; }
        public UniversityViewModel University { get; set; }
        public List<CertificateViewModel> Certificates { get; set; }
    }
}
