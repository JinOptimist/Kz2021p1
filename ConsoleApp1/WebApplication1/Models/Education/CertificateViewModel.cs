using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Education
{
    public class CertificateViewModel
    {
        public string CertificateType { get; set; }
        public string CertificateImgUrl { get; set; }
        public IFormFile CertificateImgFile { get; set; }
    }
}
