using MOJ.ProductManagement.Domain.Exceptions;

namespace MOJ.ProductManagement.Domain.ValueObjects
{
    public record QuantityPerUnit(string Value)
    {
        public static readonly string[] AllowedValues = ["Kilo", "box", "can", "liter", "bottle"];

        public static QuantityPerUnit From(string value)
        {
            if (!AllowedValues.Contains(value))
                throw new DomainException("Invalid QuantityPerUnit");
            return new QuantityPerUnit(value);
        }
    }
}
