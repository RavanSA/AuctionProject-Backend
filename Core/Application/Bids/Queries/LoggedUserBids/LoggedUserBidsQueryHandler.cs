using Application.Bids.Queries.Details;
using Application.Bids.Queries.LoggedUserBids.Extensions;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Items.Queries.List;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Bids.Queries.LoggedUserBids
{
    public class LoggedUserBidsQueryHandler : IRequestHandler<LoggedUserBidsQuery,
        PagedResponse<LoggedUserBidsResponseModel>>
    {
        private readonly IAuctionSystemDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public LoggedUserBidsQueryHandler(IAuctionSystemDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<PagedResponse<LoggedUserBidsResponseModel>> Handle(LoggedUserBidsQuery request, CancellationToken cancellationToken)
        {
            var getUser = _currentUserService.UserId;

            var queryable = _context.Bids.Include(x=>x.Item).AsNoTracking().Where(x=>x.UserId == getUser);
            
            queryable = queryable.ApplyFilters(request);

            queryable = queryable.ApplySorting(request);
            
            var totalItemsCount = await queryable.CountAsync(cancellationToken);

            var skip = (request.Page - 1) * request.PageSize;

            var list = await queryable
                .Skip(skip)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var items = list
                .Select(_mapper.Map<LoggedUserBidsResponseModel>)
                .ToList();


            var result = PaginationHelper.CreatePaginatedBidResponse(items, totalItemsCount);
            return result;

        }
    }
}
