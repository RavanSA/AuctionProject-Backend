namespace Application.Categories.Queries.List
{
    using Common.Models;
    using MediatR;

    public class CategoriesQuery : IRequest<MultiResponse<CategoriesResponseModel>> { }
}