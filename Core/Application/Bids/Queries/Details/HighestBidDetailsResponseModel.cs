namespace Application.Bids.Queries.Details
{
    using System;
    using Domain.Entities;
    using global::Common.AutoMapping.Interfaces;

    public class HighestBidDetailsResponseModel : IMapWith<Bid>
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public string UserId { get; set; }

        public string CreatedBy { get; set; }

        public Guid ItemId { get; set; }

    }
}