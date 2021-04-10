using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.EfStuff.Model
{
	[Table("PoliceAcademy", Schema = "Police")]
	public class PoliceAcademy : BaseModel
	{
		
		[Required(ErrorMessage = "Please enter your first name")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Please enter your last name")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "Please enter your birth day")]
		public DateTime BirthDate { get; set; }

		[StringLength(50)]
		[DataType(DataType.EmailAddress)]
		[RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
			ErrorMessage = "The email address is not entered in a correct format")]
		public string EMail { get; set; }

		[Required(ErrorMessage = "Please enter your phone number")]
		[StringLength(25)]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }

		public RequestStatus RequestStatus { get; set;}
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public long CitizenId { get; set; } = 0;
		[JsonIgnore]
		public virtual Citizen Citizen { get; set; }
	}
}
