namespace Application.Items.Queries.Details
{
    using System;
    using System.Collections.Generic;
    using Domain.Entities;
    using Common.Models;
    using global::Common.AutoMapping.Interfaces;
    using Pictures;

    public class GetUserUserInfoByIdQueryResponseModel : IMapWith<AuctionUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address{ get; set; }

        public string Birthday{ get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string PostCode { get; set; }

        public string ProfilePicture { get; set; }

        public string Title { get; set; }


    }
}