namespace Application.Items.Queries.List
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Helpers;
    using Common.Interfaces;
    using Common.Models;
    using Domain.Entities;
    using global::Common;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class ListItemsQueryHandler : IRequestHandler<ListItemsQuery, PagedResponse<ListItemsResponseModel>>
    {
        private readonly IAuctionSystemDbContext context;
        private readonly IMapper mapper;

        public ListItemsQueryHandler(IAuctionSystemDbContext context, IDateTime dateTime, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }



        public async Task<PagedResponse<ListItemsResponseModel>> Handle(
      ListItemsQuery request,
      CancellationToken cancellationToken)
        {
           
            var queryable = this.context
                .Items
                .OrderByDescending(b => b.Created)
                .AsQueryable();

            var totalItemsCount = await this.context.Bids.CountAsync(cancellationToken);


            totalItemsCount = await queryable.CountAsync(cancellationToken);
            var bidList = await queryable
                .ToListAsync(cancellationToken);

            var bids = bidList
                .Select(this.mapper.Map<ListItemsResponseModel>)
                .ToList();

            var result = PaginationHelper.CreatePaginatedBidResponse(bids, totalItemsCount);
            return result;
        }
    }
}