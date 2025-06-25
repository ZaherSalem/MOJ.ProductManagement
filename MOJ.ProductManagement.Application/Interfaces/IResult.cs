using MOJ.ProductManagement.Domain.Exceptions;

namespace MOJ.ProductManagement.Application.Interfaces
{
    public interface IResult<T>
    {
        List<string> Messages { get; set; }

        bool Succeeded { get; set; }

        T Data { get; set; }

        DomainException Exception { get; set; }

        int Code { get; set; }
    }
}
