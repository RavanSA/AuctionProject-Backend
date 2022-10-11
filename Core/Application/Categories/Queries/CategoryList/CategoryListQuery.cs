namespace Application.Categories.Queries.CategoryList
{
    using Common.Models;
    using MediatR;

    public class CategoryListQuery : IRequest<MultiResponse<CategoryResponseModel>> { }
}