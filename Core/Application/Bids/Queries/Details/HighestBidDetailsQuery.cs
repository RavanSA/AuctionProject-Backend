namespace Application.Bids.Queries.Details
{
    using System;
    using Common.Models;
    using MediatR;

    public class HighestBidDetailsQuery : IRequest<Response<HighestBidDetailsResponseModel>>
    {
        public HighestBidDetailsQuery(Guid itemId)
        {
            this.ItemId = itemId;
        }

        public Guid ItemId { get; }
    }
}