namespace Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.Bids.Commands.CreateBid;
    using Application.Bids.Queries.Details;
    using Microsoft.AspNetCore.Mvc;

    public class BidsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBidCommand model)
        {
            await this.Mediator.Send(model);
            return this.NoContent();
        }

        [HttpGet]
        [Route("getHighestBid/{itemId?}")]
        public async Task<IActionResult> GetHighestBid(Guid itemId)
        {
            var result = await this.Mediator.Send(new HighestBidDetailsQuery(itemId));
            return this.Ok(result);
        }


        [HttpGet]
        [Route("bidhistory/{itemId?}")]
        public async Task<IActionResult> BidHistory(Guid itemId)
        {
            var result = await this.Mediator.Send(new BidHistoryQuery(itemId));
            return this.Ok(result);
        }
    }
}