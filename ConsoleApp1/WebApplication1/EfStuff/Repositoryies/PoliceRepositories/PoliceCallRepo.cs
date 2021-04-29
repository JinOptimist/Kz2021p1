using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies
{
	public class PoliceCallRepo : BaseRepository<PoliceCallHistory>
	{
		public PoliceCallRepo(KzDbContext kzDbContext) : base(kzDbContext)
		{

		}

		public void CreateWithoutSave(PoliceCallHistory policeCall)
		{
			_kzDbContext.Add(policeCall);
		}

		public bool SavePoliceCall() {
			return _kzDbContext.SaveChanges() > 0;
		}

	}
}
