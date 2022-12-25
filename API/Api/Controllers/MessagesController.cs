namespace Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Api.Hubss;
    using Application.Message.Queries.MessageHistory;
    using Application.Messages.Commands.CreateMessage;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    public class MessagesController : BaseController
    {
        private IHubContext<MessageHub, IMessageHubClient> _messageHub;

        public MessagesController (IHubContext<MessageHub, IMessageHubClient> messageHub)
        {
            _messageHub = messageHub;
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateMessageCommand model)
        {
            _messageHub.Clients.All.SendMessageToUser(model.Message);
            var result = await this.Mediator.Send(model);
            return this.CreatedAtAction(nameof(this.Post), result);
        }

        [HttpGet("{itemId}")]
        public async Task<IActionResult> Get(Guid itemId)
        {
            var result = await this.Mediator.Send(new MessageHistoryQuery(itemId));
            return this.Ok(result);
        }



    }
}