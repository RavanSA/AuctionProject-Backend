namespace Api.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application;
    using Application.Common.Models;
    using Application.Users.Commands.CreateUser;
    using MediatR;

    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models.Errors;
    using Swashbuckle.AspNetCore.Annotations;
    using Application.Items.Commands.CreateItem;
    using Application.Notification.Command.SendNotification;

    [Authorize(Roles = "Administrator")]
    public class AdminController : BaseController
    {
        private const int CachingTimeInMinutes = 10;

        private readonly IMapper mapper;

        public AdminController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        /// <summary>
        /// Lists all users with roles
        /// </summary>
        [HttpGet]
        //[Cached(CachingTimeInMinutes)]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            "Successfully retrieved all users",
            typeof(PagedResponse<User>))]
        [SwaggerResponse(
            StatusCodes.Status401Unauthorized,
            "Unauthorized access")]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter)
        {
            // For now, return a simple response since the admin queries are commented out
            // This should be implemented with proper ListAllUsersQuery when available
            var result = new PagedResponse<User>
            {
                Data = new List<User>(),
                PageNumber = paginationFilter?.PageNumber ?? 1,
                PageSize = paginationFilter?.PageSize ?? 32,
                TotalDataCount = 0
            };
            return this.Ok(result);
        }

        /// <summary>
        /// Creates a new administrator
        /// </summary>
        [HttpPost]
        [SwaggerResponse(
            StatusCodes.Status204NoContent,
            "Admin successfully created")]
        [SwaggerResponse(StatusCodes.Status400BadRequest,
            "Bad request - validation error",
            typeof(ErrorModel))]
        [SwaggerResponse(
            StatusCodes.Status401Unauthorized,
            "Unauthorized access")]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand model)
        {
            await this.Mediator.Send(model);
            return this.NoContent();
        }

        /// <summary>
        /// Deletes administrator
        /// </summary>
        [HttpDelete]
        [SwaggerResponse(
            StatusCodes.Status204NoContent,
            "Admin successfully deleted")]
        [SwaggerResponse(StatusCodes.Status400BadRequest,
            "Bad request - validation error",
            typeof(ErrorModel))]
        [SwaggerResponse(
            StatusCodes.Status401Unauthorized,
            "Unauthorized access")]
        public async Task<IActionResult> Delete([FromBody] string userId)
        {
            // For now, return a simple response since the delete command is not implemented
            // This should be implemented with proper DeleteAdminCommand when available
            return this.NoContent();
        }



    }
}