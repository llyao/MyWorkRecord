using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
	public class RoleModel
	{
		[Required]
		[Display(Name = "Email address")]
		public string EmailAddress { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Display(Name = "Remember on this computer")]
		public bool RememberMe { get; set; }

		public int RoleType;
	}
}