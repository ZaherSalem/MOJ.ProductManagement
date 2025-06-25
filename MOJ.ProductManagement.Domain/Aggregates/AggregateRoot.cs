namespace MOJ.ProductManagement.Domain.Aggregates
{
    public abstract class AggregateRoot<TId> where TId : notnull
    {
        public TId Id { get; protected set; }
    }
}