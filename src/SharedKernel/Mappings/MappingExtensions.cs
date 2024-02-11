using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using SharedKernel.Models;

namespace SharedKernel.Mappings;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
    {
        return PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
    }

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
    {
        return queryable.ProjectTo<TDestination>(configuration).ToListAsync();
    }
}