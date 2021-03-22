using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using S4C.BL.Services;
using S4C.DAL.Models;
using S4S.Web.Models;

namespace S4S.Web.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LicenseController: ControllerBase
	{
		private readonly ILicenseService _licenseService;
		private readonly ILogger<LicenseController> _logger;
		private readonly IMapper _mapper;

		public LicenseController(ILicenseService licenseService, ILogger<LicenseController> logger, IMapper mapper)
		{
			_licenseService = licenseService;
			_logger = logger;
			_mapper = mapper;
		}

		// GET: api/list
		[HttpGet("list")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IEnumerable<LicenseOverviewViewModel>> GetLicenseList(CancellationToken cancellationToken = default)
		{
			var list = await _licenseService.GetLicenseList(cancellationToken);
			var result = _mapper.Map<IEnumerable<License>, IEnumerable<LicenseOverviewViewModel>>(list);
			return result;
		}
		
		// POST: api/Todos
		[HttpPost("add")]
		public async Task<IActionResult> CreateTodo([FromBody] LicenseRequest newTodo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			await using var stream = new FileStream(newTodo.filePath, FileMode.Open, FileAccess.Read);
			using var reader = new StreamReader(stream);
			string xmlString = await reader.ReadToEndAsync();

			bool isCreated = await _licenseService.CreateLicense(xmlString, newTodo.Name);
			if (isCreated == false)
			{
				return BadRequest();
			}

			return Ok();
		}
		
		[HttpPost("upload"), DisableRequestSizeLimit]
		public IActionResult Upload()
		{
			try
			{
				var file = Request.Form.Files[0];
				var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");

				if (file.Length > 0)
				{
					var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
					var fullPath = Path.Combine(pathToSave, fileName);
					var dbPath = Path.Combine(pathToSave, fileName);

					using (var stream = new FileStream(fullPath, FileMode.Create))
					{
						file.CopyTo(stream);
					}

					return Ok(new { dbPath });
				}
				else
				{
					return BadRequest();
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex}");
			}
		}
	}

	
}