namespace Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.Pictures.Commands.CreatePicture;
    using Application.Pictures.Queries;
    using Microsoft.AspNetCore.Mvc;

    public class PicturesController : BaseController
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await this.Mediator.Send(new PictureDetailsQuery(id));
            return this.Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePictureCommand model)
        {
            var result = await this.Mediator.Send(model);
            return this.Ok(result);
        }

    }
}