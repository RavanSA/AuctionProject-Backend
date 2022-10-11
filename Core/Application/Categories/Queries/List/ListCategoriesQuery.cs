namespace Application.Categories.Queries.List
{
    using Common.Models;
    using MediatR;

    public class ListCategoriesListQuery : IRequest<MultiResponse<ListCategoriesResponseModel>> { }
}