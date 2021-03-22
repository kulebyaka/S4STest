using System.Collections.Generic;

namespace S4C.DAL.Models
{
	public class License : IDbEntity<int>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Salt { get; set; } 
		public string Hash { get; set; }
		public List<Product> Products { get; set; }
	}
}