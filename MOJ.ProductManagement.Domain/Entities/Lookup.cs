namespace MOJ.ProductManagement.Domain.Entities
{
    public class Lookup : BaseEntity
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public virtual Lookup Parent { get; set; }
        public virtual ICollection<Lookup> Children { get; set; } = new List<Lookup>();
    }
}
