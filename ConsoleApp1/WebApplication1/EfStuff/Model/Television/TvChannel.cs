using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Television
{
    public class TvChannel : BaseModel
    {
        public string Name { get; set; }
        public DateTime WorkingFrom { get; set; }

        public virtual List<TvProgramme> Programmes { get; set; }
        public virtual List<TvStaff> Staff { get; set; }
    }
}
