using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class RolesResto:BaseModel
    {
      
        public string Name { get; set; }
        public List<UsersResto> Users { get; set; }
        public RolesResto()
        {
            Users = new List<UsersResto>();
        }
    }
}
