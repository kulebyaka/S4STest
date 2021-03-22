namespace S4C.DAL.Models
{
	public interface IDbEntity<TKey>
	{
		public TKey Id { get; set; }
	}
}