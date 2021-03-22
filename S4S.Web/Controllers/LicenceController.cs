using System;
using System.IO;
using System.Net.Http.Headers;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using S4C.BL;
using S4C.BL.Services;

namespace S4S.Web.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LicenceController: ControllerBase
	{
		private readonly ILicenseService _licenceService;
		private readonly ILogger<LicenceController> _logger;
		private readonly IMapper _mapper;

		public LicenceController(ILicenseService licenceService, ILogger<LicenceController> logger, IMapper mapper)
		{
			_licenceService = licenceService;
			_logger = logger;
			_mapper = mapper;
		}

		[HttpPost, DisableRequestSizeLimit]
		public IActionResult Upload()
		{
			try
			{
				IFormFile file = Request.Form.Files[0];
				var folderName = Path.Combine("Resources", "Images");
				var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
				if (file.Length > 0)
				{
					var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
					var fullPath = Path.Combine(pathToSave, fileName);
					var dbPath = Path.Combine(folderName, fileName);
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