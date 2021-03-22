using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using S4C.DAL.Models;

namespace S4C.BL.Services
{
	public interface ILicenseService
	{
		Task<bool> CreateLicense(string xmllicense, string name, CancellationToken cancellationToken = default);
		Task<IList<License>> GetLicenseList(CancellationToken cancellationToken = default );
		Task<License> GetLicenseById(Guid id, CancellationToken cancellationToken = default);
	}
}