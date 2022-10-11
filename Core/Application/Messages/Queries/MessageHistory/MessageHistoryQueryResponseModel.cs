namespace Application.Message.Queries.MessageHistory
{
    using System;
    using Domain.Entities;
    using global::Common.AutoMapping.Interfaces;

    public class MessageHistoryQueryResponseModel : IMapWith<Messages>
    {
        public string Id { get; set; }

        public string Message { get; set; }


        public string ItemId { get; set; }
        public string SellerId { get; set; }
        public string BidderId { get; set; }

        public string MessageOwner { get; set; }

        public DateTime Created { get; set; }
    }
}