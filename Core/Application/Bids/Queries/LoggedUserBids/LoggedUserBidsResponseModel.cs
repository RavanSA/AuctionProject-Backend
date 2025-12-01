using Common.AutoMapping.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Bids.Queries.LoggedUserBids
{
    public class LoggedUserBidsResponseModel : IMapWith<Bid>
    {
        public decimal Amount { get; set; }

        public string UserId { get; set; }

        public string CreatedBy { get; set; }

        public Guid ItemId { get; set; }

        public string ItemTitle { get; set; }

        public string ItemDescription { get; set; }


    }
}
