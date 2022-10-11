namespace Application.Message.Queries.MessageHistory
{
    using System;
    using Common.Models;
    using MediatR;

    public class MessageHistoryQuery : IRequest<PagedResponse<MessageHistoryQueryResponseModel>>
    {
        public MessageHistoryQuery(Guid itemId)
        {
            this.ItemId = itemId;
        }

        public Guid ItemId { get; set; }
    }
}