namespace Application.Bids.Queries.Details
{
    using System;
    using Common.Models;
    using MediatR;

    public class BidHistoryQuery : IRequest<PagedResponse<BidHistoryQueryResponseModel>>
    {
        public BidHistoryQuery(Guid itemId)
        {
            this.ItemId = itemId;
        }

        public Guid ItemId { get; set; }
    }
}