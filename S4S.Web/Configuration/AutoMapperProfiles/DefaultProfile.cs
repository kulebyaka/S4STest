using AutoMapper;
using S4C.BL;
using S4C.DAL.Models;
using S4S.Web.Models;

namespace S4S.Web.Configuration.AutoMapperProfiles
{
	public class DefaultProfile : Profile
	{
		public DefaultProfile()
		{
			CreateMap<License, LicenseOverviewViewModel>();
			CreateMap<License, LicenseViewModel>();
			CreateMap<Product, ProductViewModel>();
		}
	}
}