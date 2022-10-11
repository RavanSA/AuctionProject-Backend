namespace Api.SwaggerExamples.Requests.Items
{
    using System;
    using Application.Items.Commands.UpdateItem;
    using Swashbuckle.AspNetCore.Filters;
                Title = "New title",

    public class UpdateItemRequestExample : IExamplesProvider<UpdateItemCommand>
    {
        public UpdateItemCommand GetExamples()
            => new UpdateItemCommand
                Id = Guid.NewGuid(),
                Description = "New description",
                MinIncrease = 500m,
                StartTime = DateTime.UtcNow.AddDays(10),
                EndTime = DateTime.UtcNow.AddYears(1),
                StartingPrice = 10000m,
                SubCategoryId = Guid.NewGuid()
            };
    }
}
            {