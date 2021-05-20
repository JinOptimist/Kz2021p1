using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Television
{
    public class TvStaff : BaseModel
    {
        public Occupation Occupation { get; set; }

        public long CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }

        public virtual List<TvProgrammeStaff> Programme { get; set; }

        public virtual TvChannel Channel { get; set; }
    }
}
