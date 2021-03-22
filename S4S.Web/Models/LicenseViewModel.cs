using System;
using System.Collections.Generic;

namespace S4S.Web.Models
{
	public class LicenseViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Salt { get; set; } 
		public string Hash { get; set; }
		IList<ProductViewModel> Products { get; set; }
	}
}