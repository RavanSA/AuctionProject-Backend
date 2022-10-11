namespace Api.Controllers
{
    using System.Threading.Tasks;
    using Application.Categories.Queries.CategoryList;
    //using Api.Common;
    using Application.Categories.Queries.List;
    using Application.Common.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using SwaggerExamples;
    using Swashbuckle.AspNetCore.Annotations;

    public class CategoriesController : BaseController
    {

        [HttpGet]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            SwaggerDocumentation.CategoriesConstants.SuccessfulGetRequestMessage,
            typeof(MultiResponse<ListCategoriesResponseModel>))]
        [SwaggerResponse(
            StatusCodes.Status404NotFound,
            SwaggerDocumentation.CategoriesConstants.BadRequestDescriptionMessage,
            typeof(NotFoundErrorModel))]
        public async Task<IActionResult> Get()
        {
            var result = await this.Mediator.Send(new ListCategoriesListQuery());
            return this.Ok(result);
        }


        [HttpGet]
        [Route("CategoryList")]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            SwaggerDocumentation.ItemConstants.SuccessfulGetRequestMessage,
            typeof(MultiResponse<CategoryResponseModel>))]
        public async Task<IActionResult> GetCategory()
        {
            var result = await this.Mediator.Send(new CategoryListQuery());
            return this.Ok(result);
        }

    }
}