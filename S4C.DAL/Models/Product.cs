namespace S4C.DAL.Models
{
	public class Product: IDbEntity<int>
	{
		public int Id  { get; set; }
		public string Name  { get; set; }
	}
}