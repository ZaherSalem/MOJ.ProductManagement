using MOJ.ProductManagement.Application.Interfaces;
using MOJ.ProductManagement.Domain.Exceptions;

namespace MOJ.ProductManagement.Application.DTOs.Common
{
    public class Result<T> : IResult<T>
    {
        public List<string> Messages { get; set; } = new List<string>();

        public bool Succeeded { get; set; }
        public T Data { get; set; }

        public DomainException Exception { get; set; }

        public int Code { get; set; }

        #region Non Async Methods 

        #region Success Methods 

        public static Result<T> Success(int code = 200)
        {
            return new Result<T>
            {
                Succeeded = true,
                Code = code
            };
        }

        public static Result<T> Success(string message, int code = 200)
        {
            return new Result<T>
            {
                Succeeded = true,
                Messages = new List<string> { message },
                Code = code
            };
        }

        public static Result<T> Success(T data, int code = 200)
        {
            return new Result<T>
            {
                Succeeded = true,
                Data = data,
                Code = code
            };
        }

        public static Result<T> Success(T data, string message, int code = 200)
        {
            return new Result<T>
            {
                Succeeded = true,
                Messages = new List<string> { message },
                Data = data,
                Code = code
            };
        }

        #endregion

        #region Failure Methods 

        public static Result<T> Failure(IEnumerable<string> messages, int code = 400)
        {
            return new Result<T>
            {
                Succeeded = false,
                Messages = messages.ToList(),
                Code = code // Set the HTTP status code
            };
        }

        public static Result<T> Failure(string message, int code = 400)
        {
            return new Result<T>
            {
                Succeeded = false,
                Messages = new List<string> { message },
                Code = code // Set the HTTP status code
            };
        }

        public static Result<T> Failure(List<string> messages, int code = 400)
        {
            return new Result<T>
            {
                Succeeded = false,
                Messages = messages,
                Code = code // Set the HTTP status code
            };
        }

        public static Result<T> Failure(T data, string message, int code = 400)
        {
            return new Result<T>
            {
                Succeeded = false,
                Messages = new List<string> { message },
                Data = data,
                Code = code // Set the HTTP status code
            };
        }

        public static Result<T> Failure(T data, List<string> messages, int code = 400)
        {
            return new Result<T>
            {
                Succeeded = false,
                Messages = messages,
                Data = data,
                Code = code // Set the HTTP status code
            };
        }

        public static Result<T> Failure(Exception exception, int code = 500)
        {
            return new Result<T>
            {
                Succeeded = false,
                Exception = new DomainException(
                    exception.Message,
                    exception.InnerException
                ),
                Code = code // Set the HTTP status code (default: 500 for server error)
            };
        }

        #endregion

        #endregion

        #region Async Methods 

        #region Success Methods 

        public static Task<Result<T>> SuccessAsync(int code = 200)
        {
            return Task.FromResult(Success(code));
        }

        public static Task<Result<T>> SuccessAsync(string message, int code = 200)
        {
            return Task.FromResult(Success(message, code));
        }

        public static Task<Result<T>> SuccessAsync(T data, int code = 200)
        {
            return Task.FromResult(Success(data, code));
        }

        public static Task<Result<T>> SuccessAsync(T data, string message, int code = 200)
        {
            return Task.FromResult(Success(data, message, code));
        }

        #endregion

        #region Failure Methods 

        public static Task<Result<T>> FailureAsync(string message, int code = 400)
        {
            return Task.FromResult(Failure(message, code));
        }

        public static Task<Result<T>> FailureAsync(List<string> messages, int code = 400)
        {
            return Task.FromResult(Failure(messages, code));
        }

        public static Task<Result<T>> FailureAsync(T data, string message, int code = 400)
        {
            return Task.FromResult(Failure(data, message, code));
        }

        public static Task<Result<T>> FailureAsync(T data, List<string> messages, int code = 400)
        {
            return Task.FromResult(Failure(data, messages, code));
        }

        public static Task<Result<T>> FailureAsync(Exception exception, int code = 500)
        {
            return Task.FromResult(Failure(exception, code));
        }

        #endregion

        #endregion
    }
}
