namespace Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Api.Hubss;
    using Application.Bids.Commands.CreateBid;
    using Application.Bids.Queries.Details;
    using Application.Message.Queries.MessageHistory;
    using Application.Messages.Commands.CreateMessage;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using SwaggerExamples;
    using Swashbuckle.AspNetCore.Annotations;

    public class MessagesController : BaseController
    {
        private IHubContext<MessageHub, IMessageHubClient> _messageHub;
        //private IHubContext<MessageHub, IMessageHubClient> messageHub;

        public MessagesController (IHubContext<MessageHub, IMessageHubClient> messageHub)
        {
            _messageHub = messageHub;
        }


        [HttpPost]
        [SwaggerResponse(StatusCodes.Status204NoContent,
            SwaggerDocumentation.BidConstants.SuccessfulPostRequestDescriptionMessage)]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            SwaggerDocumentation.BidConstants.BadRequestOnPostRequestDescriptionMessage,
            typeof(BadRequestErrorModel))]
        [SwaggerResponse(
            StatusCodes.Status401Unauthorized,
            SwaggerDocumentation.UnauthorizedDescriptionMessage)]
        public async Task<IActionResult> Post(CreateMessageCommand model)
        {
            _messageHub.Clients.All.SendMessageToUser(model.Message);
            var result = await this.Mediator.Send(model);
            return this.CreatedAtAction(nameof(this.Post), result);
        }

        [HttpGet("{itemId}")]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            SwaggerDocumentation.BidConstants.GetHighestBidDescriptionMessage,
            typeof(GetHighestBidDetailsResponseModel))]
        public async Task<IActionResult> Get(Guid itemId)
        {
            var result = await this.Mediator.Send(new MessageHistoryQuery(itemId));
            return this.Ok(result);
        }



    }
}