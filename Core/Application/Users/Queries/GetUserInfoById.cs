namespace Application.Items.Queries.Details
{
    using System;
    using Common.Models;
    using MediatR;

    public class GetUserInfoById : IRequest<Response<GetUserUserInfoByIdQueryResponseModel>>
    {
        public GetUserInfoById(string id)
        {
            this.Id = id;
        }

        public string Id { get; set; }
    }
}