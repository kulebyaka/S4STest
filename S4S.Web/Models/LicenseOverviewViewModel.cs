using System;
using System.Collections.Generic;

namespace S4S.Web.Models
{
	public class LicenseOverviewViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public uint ProductCount { get; set; }
	}
	
	public class LicenseRequest
	{
		public string Name { get; set; }
		public string filePath { get; set; }
	}
}