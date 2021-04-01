using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class UsersRestoRepository : BaseRepository<UsersResto>
    {
        public UsersRestoRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }
        public List<UsersResto> GetAllU()
        {
            return _dbSet.Include(u=>u.Role).ToList();
        }
    }
}
