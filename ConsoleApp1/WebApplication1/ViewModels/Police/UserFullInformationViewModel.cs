using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.ViewModels
{
    public class UserFullInformationViewModel
    {
		public UserInfoViewModel UserInfo { get; set; }
		public List<UserViolationViewModel> UserViolations { get; set; }
	}
}
