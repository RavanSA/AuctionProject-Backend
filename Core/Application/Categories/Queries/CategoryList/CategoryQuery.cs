namespace Application.Categories.Queries.CategoryList
{
    using Common.Models;
    using MediatR;

    public class CategoryQuery : IRequest<MultiResponse<CategoryResponseModel>> { }
}