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
			CreateMap<License, LicenseOverviewViewModel>()
				.ForMember(a=>a.ProductCount, 
					f=>f.MapFrom(b=>b.Products.Count));

			CreateMap<License, LicenseViewModel>();
			
			CreateMap<Product, ProductViewModel>();
		}
	}
}