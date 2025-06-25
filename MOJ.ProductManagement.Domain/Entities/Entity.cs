namespace MOJ.ProductManagement.Domain.Entities
{
    public abstract class Entity<TKey>
    {
        public TKey Id { get; set; }
    }
}
