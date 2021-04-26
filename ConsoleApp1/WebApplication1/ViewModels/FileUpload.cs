using Microsoft.AspNetCore.Http;

namespace WebApplication1.ViewModels
{
	public class FileUpload
    {
		public long CitizenId { get; set; }
		public IFormFile File { get; set; }
	}
}
