using Application.Items.Queries.List;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Items.Queries.LoggedUserItems.Extensions
{
    public static class LoggedUserQueryableExtension
    {
        public static IQueryable<Item> ApplyFilters(this IQueryable<Item> query, LoggedUserItemsQuery request)
        {
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x =>
                    x.Title.ToLower().Contains(request.Search.ToLower()) ||
                    x.Description.ToLower().Contains(request.Search.ToLower()));
            }

            if (request.CategoryId.HasValue)
                query = query.Where(x => x.CategoryId == request.CategoryId.Value);

            if (request.SubCategoryId.HasValue)
                query = query.Where(x => x.SubCategoryId == request.SubCategoryId.Value);

            if (request.MinPrice.HasValue)
                query = query.Where(x => x.StartingPrice >= request.MinPrice.Value);

            if (request.MaxPrice.HasValue)
                query = query.Where(x => x.StartingPrice <= request.MaxPrice.Value);

            return query;
        }

        public static IQueryable<Item> ApplySorting(this IQueryable<Item> query, LoggedUserItemsQuery request)
        {
            return request.SortBy?.ToLower() switch
            {
                "title" => request.SortDesc ? query.OrderByDescending(x => x.Title) : query.OrderBy(x => x.Title),
                "price" => request.SortDesc ? query.OrderByDescending(x => x.StartingPrice) : query.OrderBy(x => x.StartingPrice),
                "starttime" => request.SortDesc ? query.OrderByDescending(x => x.StartTime) : query.OrderBy(x => x.StartTime),
                _ => request.SortDesc ? query.OrderByDescending(x => x.Created) : query.OrderBy(x => x.Created)
            };
        }
    }
}
