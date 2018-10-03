namespace HodorVault.Data.Models
{
    public interface IDbItem
    {
        int Id { get; set; }

    }

    public abstract class DbItem : IDbItem
    {
        public int Id { get; set; }
    }
}
