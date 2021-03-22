using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using S4C.DAL.Models;

namespace S4C.BL.Services
{
	public interface ILicenseService
	{
		Task<bool> CreateLicense(string xmlLicence);
		Task<IList<License>> GetLicenseList();
		Task<License> GetLicenseById(Guid id);
	}
}