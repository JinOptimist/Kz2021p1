using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PupilViewModel : PersonViewModel
    {
        public string Subject { get; set; }
        public int ClassYear { get; set; }
        public double AverageMark { get; set; }
        public DateTime? GraduatedYear { get; set; }
        public int? ENT { get; set; }
        public long? SchoolId { get; set; }
        public SchoolViewModel School { get; set; }
    }
}
