using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MOJ.ProductManagement.Application.DTOs.Common;

namespace MOJ.ProductManagement.Application.Extensions
{
    public static class QueryableExtensions
    {
        //IQueryable
        public static async Task<PaginatedResult<TResponse>> ToPaginatedListAsync<T, TResponse>(this IQueryable<T> source, IMapper mapper, PaginatedRequest dto, CancellationToken cancellationToken)
            where T : class
            where TResponse : class
        {

            int count = await source.CountAsync();

            List<TResponse> items = await source.Skip((dto.PageNumber - 1) * dto.PageSize)
                                                .Take(dto.PageSize)
                                                .ProjectTo<TResponse>(mapper.ConfigurationProvider)
                                                .AsNoTracking()
                                                .ToListAsync(cancellationToken);

            return PaginatedResult<TResponse>.Create(items, count, dto.PageNumber, dto.PageSize);
        }
    }
}