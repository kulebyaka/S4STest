using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using S4C.DAL.Models;
using S4C.DAL.Repositories;

namespace S4C.BL.Services
{
	public class LicenseService : ILicenseService
	{
		private readonly IRepository<License> _licensesRepository;
		// private readonly ILogger<LicenseService> _logger;
		private readonly ILicenseXmlFactory<License> _factory;

		public LicenseService(IRepository<License> licensesRepository, ILicenseXmlFactory<License> factory
			// , ILogger<LicenseService> logger
			)
		{
			_licensesRepository = licensesRepository;
			_factory = factory;
			// _logger = logger;
		}

		public async Task<bool> CreateLicense(string xmlString, string name, CancellationToken cancellationToken = default)
		{
			License xmlLicense = _factory.CreateXmlLicense(xmlString);
			if (xmlLicense == null)
				return false;
			xmlLicense.Name = name;
			await _licensesRepository.AddAsync(xmlLicense);
			return true;
		}

		public async Task<IList<License>> GetLicenseList(CancellationToken cancellationToken = default)
		{
			var licenses =  await _licensesRepository.GetAllAsync(cancellationToken);
			return licenses.ToList();
		}

		public async Task<License> GetLicenseById(Guid id, CancellationToken cancellationToken = default)
		{
			License license =  await _licensesRepository.GetByIdAsync(cancellationToken);
			return license;
		}
	}

	public interface ILicenseXmlFactory<T> where T : class, new()
	{
		T CreateXmlLicense(string xmlString);
	}
	public class LicenseXmlFactory<T> : ILicenseXmlFactory<T> where T : class, new()
	{
		private IDataDeserialize<T> serializer;

		public LicenseXmlFactory(IDataDeserialize<T> serializer)
		{
			this.serializer = serializer;
		}

		public T CreateXmlLicense(string xmlString)
		{
			return serializer.Deserialize(xmlString);
		}
	}
}