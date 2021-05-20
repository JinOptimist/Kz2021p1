using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Television
{
    public class TvProgramme : BaseModel
    {
        public string Name { get; set; }
        public ContentRating ContentRating { get; set; }
        public TypeOfProgramme TypeOfProgramme { get; set; }

        public string AvatarUrl { get; set; }

        public virtual List<TvSchedule> Schedules { get; set; }
        public virtual List<TvProgrammeStaff> Staff { get; set; }
        public virtual TvChannel Channel { get; set; }

        public virtual List<TvProgrammeCelebrity> Celebrities { get; set; }
    }
}
