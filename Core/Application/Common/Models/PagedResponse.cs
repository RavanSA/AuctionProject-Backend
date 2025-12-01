namespace Application.Common.Models
{
    using System;
    using System.Collections.Generic;

    public class PagedResponse<T>
    {
        public PagedResponse()
        {
            this.PageNumber = 1;
            this.PageSize = 32;
        }

        public PagedResponse(IEnumerable<T> data, int totalDataCountInDatabase)
        {
            this.Data = data;
            this.TotalPages = (int) Math.Ceiling(totalDataCountInDatabase / (double) 24);

            if(data!=null)this.Succeeded = true;
            else this.Succeeded = false;
        }


        public PagedResponse(string Error)
        {
            Succeeded = false;
            ErrorMessage= Error;
        }


        public static PagedResponse<T> Failure(string error)
    => new PagedResponse<T>(error);

        public int TotalPages { get; set; }

        public int PageNumber { get; set; }

        public int? PageSize { get; set; }

        public int? NextPage { get; set; }

        public int? PreviousPage { get; set; }

        public IEnumerable<T> Data { get; set; }

        public int TotalDataCount { get; set; }

        public bool Succeeded { get; set; }
        public string? ErrorMessage { get; set; }
    }
}