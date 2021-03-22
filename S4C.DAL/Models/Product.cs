using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S4C.DAL.Models
{
	public class Product: IDbEntity<int>
	{
		[Key]
		public int Id  { get; set; }
		[Column(TypeName = "nvarchar(50)")]
		public string Name  { get; set; }
	}
}