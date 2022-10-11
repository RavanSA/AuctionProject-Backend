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

namespace Application.Message.Queries.MessageHistory
{

    public class MessageHistoryQueryHandler : IRequestHandler<MessageHistoryQuery, PagedResponse<MessageHistoryQueryResponseModel>>
    {
        private readonly IAuctionSystemDbContext context;
        private readonly IMapper mapper;

        public MessageHistoryQueryHandler(IAuctionSystemDbContext context, IMapper mapper, IDateTime created)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<PagedResponse<MessageHistoryQueryResponseModel>> Handle(
            MessageHistoryQuery request,
            CancellationToken cancellationToken)
        {
            var queryable = this.context
                .Messages
                .Where(b => b.ItemId == request.ItemId )
                .OrderByDescending(b => b.Created)
                .AsQueryable();

            var messageList = await queryable
          .ToListAsync(cancellationToken);

            var message = messageList
                .Select(this.mapper.Map<MessageHistoryQueryResponseModel>)
                .ToList();

            var result = PaginationHelper.MessageQueryResponse(message);
            return result;
        }
    }
}