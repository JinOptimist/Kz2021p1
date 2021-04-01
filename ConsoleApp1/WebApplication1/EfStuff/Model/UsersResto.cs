using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.EfStuff.Model
{
    public class UsersResto : BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public long? RoleId { get; set; }
        public RolesResto Role { get; set; }
    }
}
