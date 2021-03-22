using Autofac;
using S4C.BL;
using S4C.BL.Services;

namespace S4S.Web.Configuration
{
	/// <summary>
	/// Default module for Autofac
	/// </summary>
	public class DefaultModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<LicenseService>().As<ILicenseService>().InstancePerLifetimeScope();
			// builder.RegisterType<LicenseRepository>().As<IRepository<License>>().SingleInstance();
		}
	}
}