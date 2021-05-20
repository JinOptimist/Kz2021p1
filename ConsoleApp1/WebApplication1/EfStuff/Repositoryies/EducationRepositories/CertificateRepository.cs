using System.Collections.Generic;
using System.Linq;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies.Interface;

namespace WebApplication1.EfStuff.Repositoryies
{
    public class CertificateRepository : BaseRepository<Certificate>, ICertificateRepository
    {
        public CertificateRepository(KzDbContext kzDbContext) : base(kzDbContext) { }

        public Certificate GetCertificateByType(string certificateType)
        {
            return _kzDbContext.Certificates.SingleOrDefault(x => x.CertificateType == certificateType);
        }

        public List<string> GetCertificateTypes()
        {
            return _kzDbContext.Certificates.Select(x => x.CertificateType).ToList();
        }
    }
}
