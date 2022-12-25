﻿namespace Application.Common.Models
{
   // using Admin.Queries.List;
    using global::Common.AutoMapping.Interfaces;
    using Items.Queries.List;

    public class PaginationFilter : IMapWith<ListItemsQuery>
    {
        private const int DefaultPageNumber = 1;

        public PaginationFilter()
        {
            this.PageNumber = DefaultPageNumber;
            this.PageSize = 32;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < DefaultPageNumber ? DefaultPageNumber : pageNumber;
            this.PageSize = pageSize >= 32 || pageSize < 1 ? 32 : pageSize;
        }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}