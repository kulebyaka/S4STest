using Autofac;
using S4C.BL;
using S4C.BL.Services;
using S4C.DAL;
using S4C.DAL.Models;
using S4C.DAL.Repositories;

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
			builder.RegisterType<S4SContext>();
			builder.RegisterType<LicensesRepository>().As<IRepository<License>>().SingleInstance();
			builder.RegisterType<XmlLicenseSerializer>().As<IDataDeserialize<License>>().SingleInstance();
			builder
				.RegisterGeneric(typeof(LicenseXmlFactory<>))
				.As(typeof(ILicenseXmlFactory<>))
				.InstancePerDependency();
		}
	}
}