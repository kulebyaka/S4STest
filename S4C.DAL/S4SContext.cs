using Microsoft.EntityFrameworkCore;
using S4C.DAL.Models;

namespace S4C.DAL
{
	public class S4SContext : DbContext  
	{
		private readonly DbContextOptions _options;
		public S4SContext(DbContextOptions options) : base(options)
		{
			_options = options;
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(@"Server=(localdb)\\mssqllocaldb;Database=S4C;Trusted_Connection=True;MultipleActiveResultSets=true");
			}
		}
		
		public  DbSet<License> Licenses { get; set; }
		public  DbSet<Product> Products { get; set; }
  
	}   
}