namespace Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.Common.Interfaces;
    using Application.Common.Models;
    using Application.Items.Commands;
    using Application.Items.Commands.CreateItem;
    using Application.Items.Commands.DeleteItem;
    using Application.Items.Commands.UpdateItem;
    using Application.Items.Queries.Details;
    using Application.Items.Queries.List;
    using AutoMapper;
    using Domain.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using SwaggerExamples;
    using Swashbuckle.AspNetCore.Annotations;

    public class ItemsController : BaseController
    {

        private readonly IMapper mapper;

        public ItemsController(IMapper mapper)
        {
            this.mapper = mapper;

        }

        /// <summary>
        /// Retrieves all items (max 24 per request)
        /// </summary>
        [HttpGet]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            SwaggerDocumentation.ItemConstants.SuccessfulGetRequestMessage,
            typeof(PagedResponse<ListItemsResponseModel>))]
        public async Task<IActionResult> Get()
        {
            var result = await this.Mediator.Send(new ListItemsQuery());
            return this.Ok(result);
        }

        /// <summary>
        /// Retrieves item with given id
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            SwaggerDocumentation.ItemConstants.SuccessfulGetRequestWithIdDescriptionMessage,
            typeof(Response<ItemDetailsResponseModel>))]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            SwaggerDocumentation.ItemConstants.BadRequestDescriptionMessage,
            typeof(BadRequestErrorModel))]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await this.Mediator.Send(new GetItemDetailsQuery(id));
            return this.Ok(result);
        }

        /// <summary>
        /// Creates item
        /// </summary>
        [HttpPost]
        [SwaggerResponse(
            StatusCodes.Status201Created,
            SwaggerDocumentation.ItemConstants.SuccessfulPostRequestDescriptionMessage,
            typeof(Response<ItemResponseModel>))]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            SwaggerDocumentation.ItemConstants.BadRequestDescriptionMessage,
            typeof(BadRequestErrorModel))]
        [SwaggerResponse(
            StatusCodes.Status401Unauthorized,
            SwaggerDocumentation.UnauthorizedDescriptionMessage)]
        public async Task<IActionResult> Post([FromForm] CreateItemCommand model)
        {
            var result = await this.Mediator.Send(model);
            return this.CreatedAtAction(nameof(this.Post), result);
        }


        [HttpPut]
        [SwaggerResponse(
            StatusCodes.Status204NoContent,
            SwaggerDocumentation.ItemConstants.SuccessfulPutRequestDescriptionMessage)]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            SwaggerDocumentation.ItemConstants.BadRequestDescriptionMessage,
            typeof(BadRequestErrorModel))]
        [SwaggerResponse(
            StatusCodes.Status404NotFound,
            SwaggerDocumentation.ItemConstants.NotFoundDescriptionMessage,
            typeof(NotFoundErrorModel))]
        [SwaggerResponse(
            StatusCodes.Status401Unauthorized,
            SwaggerDocumentation.UnauthorizedDescriptionMessage)]
        public async Task<IActionResult> Put( [FromBody] UpdateItemCommand model)
        {

            await this.Mediator.Send(model);
            return this.NoContent();
        }

        /// <summary>
        /// Deletes item
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [SwaggerResponse(
            StatusCodes.Status204NoContent,
            SwaggerDocumentation.ItemConstants.SuccessfulDeleteRequestDescriptionMessage)]
        [SwaggerResponse(
            StatusCodes.Status404NotFound,
            SwaggerDocumentation.ItemConstants.BadRequestDescriptionMessage,
            typeof(NotFoundErrorModel))]
        [SwaggerResponse(
            StatusCodes.Status401Unauthorized,
            SwaggerDocumentation.UnauthorizedDescriptionMessage)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this.Mediator.Send(new DeleteItemCommand(id));
            return this.NoContent();
        }
    }
}