namespace Application.Messages.Commands.CreateMessage
{
    using System;
    using Domain.Entities;
    using global::Common.AutoMapping.Interfaces;
    using MediatR;

    public class CreateMessageCommand : IRequest, IMapWith<Messages>
    {

        public string Message { get; set; }

        public string ItemId { get; set; }

        public string SellerId { get; set; }


        public string BidderId { get; set; }

        public string MessageOwner { get; set; }
    }
}