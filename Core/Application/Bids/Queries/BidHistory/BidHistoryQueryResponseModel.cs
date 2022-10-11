namespace Application.Bids.Queries.Details
{
    using System;
    using Domain.Entities;
    using global::Common.AutoMapping.Interfaces;

    public class BidHistoryQueryResponseModel : IMapWith<Bid>
    {
        public string Id { get; set; }

        public decimal Amount { get; set; }

        public string UserId { get; set; }

        public string ItemId { get; set; }

        public DateTime Created { get; set; }
    }
}