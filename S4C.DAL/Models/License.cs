using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S4C.DAL.Models
{
	public class License : IDbEntity<int>
	{
		[Key]
		public int Id { get; set; }
		[Column(TypeName = "nvarchar(50)")]
		public string Name { get; set; }
		public string Salt { get; set; } 
		public string Hash { get; set; }
		public List<Product> Products { get; set; }
	}
}