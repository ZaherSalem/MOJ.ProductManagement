namespace MOJ.ProductManagement.Domain.Exceptions
{
    public class DomainException : Exception
    {
        // Constructors
        public DomainException() { }
        public DomainException(string message) : base(message) { }
        public DomainException(string message, Exception inner) : base(message, inner) { }
    }
}
