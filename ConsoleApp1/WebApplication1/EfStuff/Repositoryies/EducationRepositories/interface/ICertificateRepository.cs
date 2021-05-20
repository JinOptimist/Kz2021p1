using System.Collections.Generic;
using WebApplication1.EfStuff.Model;

namespace WebApplication1.EfStuff.Repositoryies.Interface
{
	public interface ICertificateRepository : IBaseRepository<Certificate>
    {
        Certificate GetCertificateByType(string certificateType);
        List<string> GetCertificateTypes();
    }
}
