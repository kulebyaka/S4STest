using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using S4C.BL.Services;
using S4C.DAL.Models;

namespace S4C.BL
{
	public class LicenseService : ILicenseService
	{
		public Task<bool> CreateLicense(string xmlLicence)
		{
			throw new NotImplementedException();
		}

		public Task<IList<License>> GetLicenseList()
		{
			throw new NotImplementedException();
		}

		public Task<License> GetLicenseById(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}