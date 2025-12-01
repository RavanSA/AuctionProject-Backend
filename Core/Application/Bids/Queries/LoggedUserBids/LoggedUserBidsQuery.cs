using Application.Bids.Queries.Details;
using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Bids.Queries.LoggedUserBids
{
    public class LoggedUserBidsQuery : IRequest<PagedResponse<LoggedUserBidsResponseModel>>
    {
        public string? SortBy { get; set; }  
        public bool SortDesc { get; set; } = false;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public string? Search { get; set; }

    }
}
