namespace Application.Items.Queries.List
{
    using Common.Models;
    using Domain.Entities;
    using MediatR;
    using System;

    public class ListItemsQuery : IRequest<PagedResponse<ListItemsResponseModel>>
    {
        public string? Search { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public string? SortBy { get; set; } // Created, Price, Title
        public bool SortDesc { get; set; } = false;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public ItemStatus? Status { get; set; }  
    }
}