namespace Api.Controllers
{
    using System.Threading.Tasks;
    using Application.Categories.Queries.CategoryList;
    using Application.Categories.Queries.List;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await this.Mediator.Send(new CategoryQuery());
            return this.Ok(result);
        }


        [HttpGet]
        [Route("CategoryList")]
        public async Task<IActionResult> GetCategory()
        {
            var result = await this.Mediator.Send(new CategoriesQuery());
            return this.Ok(result);
        }

    }
}