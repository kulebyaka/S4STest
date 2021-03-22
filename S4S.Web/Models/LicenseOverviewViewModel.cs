using System;
using System.Collections.Generic;

namespace S4S.Web.Models
{
	public class LicenseOverviewViewModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public uint ProductCount { get; set; }
	}
}