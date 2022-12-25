using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Bids.Queries.Details;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Bids.Queries.BidHistory
{

    public class BidHistoryQueryHandler : IRequestHandler<BidHistoryQuery, PagedResponse<BidHistoryQueryResponseModel>>
    {
        private readonly IAuctionSystemDbContext _context;
        private readonly IMapper _mapper;

        public BidHistoryQueryHandler(IAuctionSystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        public async Task<PagedResponse<BidHistoryQueryResponseModel>> Handle(
            BidHistoryQuery request,
            CancellationToken cancellationToken)
        {
            var queryable = _context
                .Bids
                .Where(b => b.ItemId == request.ItemId)
                .OrderByDescending(b => b.Created)
                .AsQueryable();

            var totalItemsCount = await _context.Bids.CountAsync(cancellationToken);
        

            totalItemsCount = await queryable.CountAsync(cancellationToken);
            var bidList = await queryable
                .ToListAsync(cancellationToken);

            var bids = bidList
                .Select(_mapper.Map<BidHistoryQueryResponseModel>)
                .ToList();

            var result = PaginationHelper.CreatePaginatedBidResponse(bids, totalItemsCount);
            return result;
        }
    }
}