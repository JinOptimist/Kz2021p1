using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Television
{
    public class TvProgrammeCelebrity:BaseModel
    {
        public virtual TvProgramme Programme { get; set; }
        public virtual TvCelebrity Celebrity { get; set; }
    }
}
