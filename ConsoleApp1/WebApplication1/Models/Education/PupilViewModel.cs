using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.Education;

namespace WebApplication1.Models
{
    public class PupilViewModel : PersonViewModel
    {
        public int ClassYear { get; set; }
        public int AverageMark { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? GraduatedYear { get; set; }
        public int? ENT { get; set; }
        public SchoolViewModel School { get; set; }
        public CertificateViewModel Certificate{ get; set; }
    }
}
