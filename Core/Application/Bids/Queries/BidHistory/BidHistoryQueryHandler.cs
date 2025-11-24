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
using Microsoft.Extensions.Logging;

namespace Application.Bids.Queries.BidHistory
{
    public class BidHistoryQueryHandler : IRequestHandler<BidHistoryQuery, PagedResponse<BidHistoryQueryResponseModel>>
    {
        private readonly IAuctionSystemDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<BidHistoryQueryHandler> _logger;

        public BidHistoryQueryHandler(
            IAuctionSystemDbContext context, 
            IMapper mapper,
            ILogger<BidHistoryQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedResponse<BidHistoryQueryResponseModel>> Handle(
            BidHistoryQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching bid history for ItemId: {ItemId}", request.ItemId);

            var queryable = _context
                .Bids
                .AsNoTracking()
                .Where(b => b.ItemId == request.ItemId)
                .OrderByDescending(b => b.Created);

            var totalItemsCount = await queryable.CountAsync(cancellationToken);
            _logger.LogInformation("Total bids found: {Count}", totalItemsCount);

            var bidList = await queryable
                .Include(b => b.User)
                .ToListAsync(cancellationToken);

            _logger.LogInformation("Bids loaded: {Count}", bidList.Count);

            var userIds = bidList.Select(b => b.UserId).Distinct().ToList();
            
            var users = await _context.Users
                .Where(u => userIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.FullName ?? string.Empty, cancellationToken);

            var bids = bidList
                .Select(b => new BidHistoryQueryResponseModel
                {
                    Id = b.Id.ToString(),
                    Amount = b.Amount,
                    UserId = b.UserId,
                    ItemId = b.ItemId.HasValue ? b.ItemId.Value.ToString() : null,
                    Created = b.Created,
                    UserFullName = users.ContainsKey(b.UserId) ? users[b.UserId] : string.Empty
                })
                .ToList();

            _logger.LogInformation("Bids mapped with user names: {Count}", bids.Count);

            var result = PaginationHelper.CreatePaginatedBidResponse(bids, totalItemsCount);
            return result;
        }
    }
}