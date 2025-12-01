using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Items.Queries.LoggedUserItems
{
    public class LoggedUserItemsQuery : IRequest<PagedResponse<LoggedUserItemsResponseModel>>
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
    }

}
