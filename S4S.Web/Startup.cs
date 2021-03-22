using System.Text.Json;
using System.Text.Json.Serialization;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using S4C.DAL;
using S4S.Web.Configuration;
using S4S.Web.Filters;

namespace S4S.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;

			JsonConvert.DefaultSettings = () =>
			{
				var settings = new JsonSerializerSettings()
				{
					ContractResolver = new CamelCasePropertyNamesContractResolver(),
					NullValueHandling = NullValueHandling.Ignore,
					DefaultValueHandling = DefaultValueHandling.Include,
					ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
#if DEBUG
					Formatting = Formatting.Indented
#else
                    Formatting = Formatting.None
#endif
				};
				settings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
				return settings;
			};
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors()
				// Add useful interface for accessing the ActionContext outside a controller.
				.AddSingleton<IActionContextAccessor, ActionContextAccessor>()
				// Add useful interface for accessing the HttpContext outside a controller.
				.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
				// Add useful interface for accessing the IUrlHelper outside a controller.
				.AddScoped<IUrlHelper>(x => x
					.GetRequiredService<IUrlHelperFactory>()
					.GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext))
				.AddMvcCore(options =>
				{
					options.Filters.Add(new ValidateModelFilter()); // Validate model.
				})
				.AddJsonOptions(options =>
				{
					options.JsonSerializerOptions.IgnoreNullValues = true;
					options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
					options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
				})
				.AddApiExplorer()
				.SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

			services
				.AddAutoMapper(typeof(Startup))
				.AddSwagger();


			services.AddRouting();
			// Add framework services.
			services.AddControllers();
			services.AddHealthChecks();

			
			services.AddDbContext<S4SContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));
			services.AddDatabaseDeveloperPageExceptionFilter();

			services.Configure<FormOptions>(o => {
				o.ValueLengthLimit = int.MaxValue;
				o.MultipartBodyLengthLimit = int.MaxValue;
				o.MemoryBufferThreshold = int.MaxValue;
			});
			
			// services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
			// 	.AddEntityFrameworkStores<ApplicationDbContext>();
			// services.AddControllersWithViews();
		}

		public void ConfigureContainer(ContainerBuilder builder)
		{
			// Add things to the Autofac ContainerBuilder.
			builder.RegisterModule<DefaultModule>();
			builder.RegisterModule(new Autofac.Configuration.ConfigurationModule(Configuration));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}
			
			// Create db if not exists
			using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetRequiredService<S4SContext>();
				context.Database.Migrate();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseCors(builder => builder
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());

			app.UseSwaggerWithOptions();
			
			// app.UseAuthentication();
			// app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
				endpoints.MapHealthChecks(Constants.Health.EndPoint);
				
				// endpoints.MapControllerRoute(
				// 	name: "default",
				// 	pattern: "{controller=Home}/{action=Index}/{id?}");
				// endpoints.MapRazorPages();
			});
		}
	}
}