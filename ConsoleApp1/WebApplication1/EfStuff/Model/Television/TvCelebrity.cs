using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model.Television
{
    public class TvCelebrity:BaseModel
    {
        public CelebrityOccupation Occupation { get; set; }

        public long CitizenId { get; set; }
        public virtual Citizen Citizen { get; set; }

        public virtual List<TvProgrammeCelebrity> Programme { get; set; }
    }
}
