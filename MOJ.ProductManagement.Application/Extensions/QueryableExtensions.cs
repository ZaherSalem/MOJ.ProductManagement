using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MOJ.ProductManagement.Application.DTOs.Common;

namespace MOJ.ProductManagement.Application.Extensions
{
    public static class QueryableExtensions
    {
        //IQueryable
        public static async Task<PaginatedResult<TResponse>> ToPaginatedListAsync<T, TResponse>(this IQueryable<T> source, IMapper mapper, int pageNumber, int pageSize, CancellationToken cancellationToken)
            where T : class
            where TResponse : class
        {
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;

            int count = await source.CountAsync();

            List<TResponse> items = await source.Skip((pageNumber - 1) * pageSize)
                                                .Take(pageSize)
                                                .ProjectTo<TResponse>(mapper.ConfigurationProvider)
                                                .AsNoTracking()
                                                .ToListAsync(cancellationToken);

            return PaginatedResult<TResponse>.Create(items, count, pageNumber, pageSize);
        }
    }
}