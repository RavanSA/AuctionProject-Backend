namespace Application.Bids.Commands.CreateBid
{
    using System;
    using Application.Common.Models;
    using Domain.Entities;
    using global::Common.AutoMapping.Interfaces;
    using MediatR;

    public class CreateBidCommand : IRequest<Result>, IMapWith<Bid>
    {
        public decimal Amount { get; set; }

        public Guid ItemId { get; set; }

        public string UserId { get; set; }
    }
}