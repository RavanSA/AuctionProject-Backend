using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Items.Queries.List;
using AutoMapper;
using Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Application.Items.Queries.LoggedUserItems.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Domain.Entities;

namespace Application.Items.Queries.LoggedUserItems
{
    internal class LoggedUserItemsQueryHandler : IRequestHandler<LoggedUserItemsQuery, PagedResponse<LoggedUserItemsResponseModel>>
    {
        private readonly IAuctionSystemDbContext context;
        private readonly IMapper mapper;
        private readonly ICurrentUserService _currentUserService;
        public LoggedUserItemsQueryHandler(IAuctionSystemDbContext context, IDateTime dateTime, IMapper mapper, ICurrentUserService currentUserService)
        {
            this.context = context;
            this.mapper = mapper;
            _currentUserService = currentUserService;
        }



        public async Task<PagedResponse<LoggedUserItemsResponseModel>> Handle(
      LoggedUserItemsQuery request,
      CancellationToken cancellationToken)
        {

            if(_currentUserService.UserId==null) return PagedResponse<LoggedUserItemsResponseModel>.Failure("User has no register");
            var queryable = this.context
                .Items
                .Where(x=>x.UserId==_currentUserService.UserId)
                .AsQueryable();


            queryable = queryable.ApplyFilters(request);

            queryable = queryable.ApplySorting(request);

            if (request.Status != null)
                queryable = queryable.Where(i => i.Status == request.Status);
            else
                queryable = queryable.Where(i => i.Status == ItemStatus.Continue);


            var totalItemsCount = await this.context.Bids.CountAsync(cancellationToken);


            //  totalItemsCount = await queryable.CountAsync(cancellationToken);
            //var bidList = await queryable
            //    .ToListAsync(cancellationToken);

            //var bids = bidList
            //    .Select(this.mapper.Map<ListItemsResponseModel>)
            //    .ToList();

            var skip = (request.Page - 1) * request.PageSize;

            var list = await queryable
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var items = list
                .Select(this.mapper.Map<LoggedUserItemsResponseModel>)
                .ToList();

            var result = PaginationHelper.CreatePaginatedBidResponse(items, totalItemsCount);
            return result;
        }
    }
}

