using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model.Television;

namespace WebApplication1.EfStuff.Repositoryies.Television
{
    public class TvProgrammeStaffRepository : BaseRepository<TvProgrammeStaff>
    {
        public TvProgrammeStaffRepository(KzDbContext kzDbContext) : base(kzDbContext)
        {
        }

        public List<TvProgrammeStaff> GetByProgrammeName(string programmeName)
        {
            return _dbSet.Where(x => x.Programme.Name == programmeName).ToList();
        }

        public List<TvProgrammeStaff> GetByStaffName(string name)
        {
            return _dbSet.Where(x => x.Staff.Citizen.Name == name).ToList();
        }
    }
}
