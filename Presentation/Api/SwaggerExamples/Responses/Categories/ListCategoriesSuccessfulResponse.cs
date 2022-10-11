namespace Api.SwaggerExamples.Responses.Categories
{
    using System;
    using System.Collections.Generic;
    using Application.Categories.Queries.CategoryList;
    using Application.Categories.Queries.List;
    using Application.Common.Models;
    using Swashbuckle.AspNetCore.Filters;

    public class ListCategoriesSuccessfulResponse : IExamplesProvider<MultiResponse<CategoryResponseModel>>
    {
        public MultiResponse<CategoryResponseModel> GetExamples()
            => new MultiResponse<CategoryResponseModel>(
                new List<CategoryResponseModel>
                {
                    new CategoryResponseModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Art",
                       
                    },
                    new CategoryResponseModel
                    {
                        Id = Guid.NewGuid(),
                        Name = "Jewelry",
                        
                    }
                });
    }
}