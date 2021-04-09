using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class SportComplex : BaseModel
    {
        public string Name { get; set; }
        public int CountOfEmployees { get; set; }
        public virtual List<SportSection> Sections { get; set; }
    }
}
