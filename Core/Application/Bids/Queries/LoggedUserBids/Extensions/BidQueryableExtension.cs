using Application.Bids.Queries.LoggedUserBids;
using Application.Items.Queries.List;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Bids.Queries.LoggedUserBids.Extensions
{
    public static class BidQueryableExtensions
    {
        public static IQueryable<Bid> ApplyFilters(this IQueryable<Bid> query, LoggedUserBidsQuery request)
        {
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(x =>
                    x.Item.Title.ToLower().Contains(request.Search.ToLower()) ||
                    x.Item.Description.ToLower().Contains(request.Search.ToLower()));
            }



            return query;
        }

        public static IQueryable<Bid> ApplySorting(this IQueryable<Bid> query, LoggedUserBidsQuery request)
        {
            return request.SortBy?.ToLower() switch
            {
                "title" => request.SortDesc ? query.OrderByDescending(x => x.Item.Title) : query.OrderBy(x => x.Item.Title),
                "price" => request.SortDesc ? query.OrderByDescending(x => x.Item.StartingPrice) : query.OrderBy(x => x.Item.StartingPrice),
                "starttime" => request.SortDesc ? query.OrderByDescending(x => x.Item.StartTime) : query.OrderBy(x => x.Item.StartTime),
                _ => request.SortDesc ? query.OrderByDescending(x => x.Created) : query.OrderBy(x => x.Created)
            };
        }
    }

}
