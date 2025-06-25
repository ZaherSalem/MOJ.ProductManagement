using FluentValidation;
using MediatR;
using MOJ.ProductManagement.Application.DTOs.Common;
using System.Net;

namespace MOJ.ProductManagement.Application.PipelineBehavior
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : class
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(next);

            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)))
                                                  .ConfigureAwait(false);

                var failures = validationResults.Where(r => r.Errors.Count > 0)
                                                .SelectMany(r => r.Errors)
                                                .ToList();

                if (failures.Count > 0)
                {
                    var errorMessages = failures.Select(f => f.ErrorMessage).ToList();
                    var genericArg = typeof(TResponse).GetGenericArguments()[0];
                    return CreateFailureResponse(errorMessages, genericArg);
                }
            }

            return await next().ConfigureAwait(false);
        }

        private TResponse CreateFailureResponse(List<string> errors, Type genericArgumentType)
        {
            if (typeof(TResponse).IsGenericType)
            {
                var genericType = typeof(TResponse).GetGenericTypeDefinition();

                // Handle PaginatedResult<T>
                if (genericType == typeof(PaginatedResult<>))
                {
                    var method = typeof(PaginatedResult<>)
                        .MakeGenericType(genericArgumentType)
                        .GetMethod(nameof(PaginatedResult<object>.Failure), new[] { typeof(List<string>), typeof(int) });

                    return (TResponse)method!.Invoke(null, new object[] { errors, (int)HttpStatusCode.BadRequest });
                }

                // Handle Result<T>
                if (genericType == typeof(Result<>))
                {
                    var method = typeof(Result<>)
                        .MakeGenericType(genericArgumentType)
                        .GetMethod(nameof(Result<object>.Failure), new[] { typeof(List<string>), typeof(int) });

                    return (TResponse)method!.Invoke(null, new object[] { errors, (int)HttpStatusCode.BadRequest });
                }
            }

            throw new InvalidOperationException($"Unsupported response type: {typeof(TResponse)}");
        }
    }
}