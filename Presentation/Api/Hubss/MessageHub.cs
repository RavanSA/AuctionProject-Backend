using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Messages.Commands.CreateMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;



namespace Api.Hubss
{
    public class MessageHub : Hub<IMessageHubClient>
    {
        private readonly IMediator mediator;
        
        public MessageHub(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task SendMessageToUser(string message)
        {
            await Clients.All.SendMessageToUser(message);
        }


        //public Task JoinGroup(string itemId)
        //{
        //    return Groups.AddToGroupAsync(Context.ConnectionId, itemId);
        //}

        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}

        //public Task SendMessageToGroup(string sender, string receiver, string message)
        //{
        //    return Clients.Group(receiver).SendAsync("ReceiveMessage", sender, message);
        //}

        //public async Task SetConversation(string itemId)
        //{
        //    if(itemId == null)
        //    {
        //        return;
        //    }

        //    await this.Groups.AddToGroupAsync(this.Context.ConnectionId, itemId);
        //}

        //public async Task SendMessageToUser(string itemId, string bidderId, string sellerId, string message)
        //{
        //    await this.mediator.Send(new CreateMessageCommand
        //    {
        //        Message = message,
        //        BidderId = bidderId,
        //        SellerId = sellerId,
        //        ItemId = itemId
        //    });

        //    await this.Clients.Groups(itemId).SendAsync("ReceiveMessage", itemId, bidderId, sellerId, message);
        //}



    }
}
