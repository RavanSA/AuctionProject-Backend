namespace Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.Items.Commands.CreateItem;
    using Application.Items.Commands.UpdateItem;
    using Application.Items.Queries.Details;
    using Application.Items.Queries.List;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
 
    public class ItemsController : BaseController
    {

        private readonly IMapper mapper;

        public ItemsController(IMapper mapper)
        {
            this.mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.Mediator.Send(new ListItemsQuery());
            return this.Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await this.Mediator.Send(new GetItemDetailsQuery(id));
            return this.Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateItemCommand model)
        {
            var result = await this.Mediator.Send(model);
            return this.CreatedAtAction(nameof(this.Post), result);
        }


        [HttpPut]
        public async Task<IActionResult> Put( [FromBody] UpdateItemCommand model)
        {

            await this.Mediator.Send(model);
            return this.NoContent();
        }

    }
}