namespace Domain.Entities
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class AuctionUser : IdentityUser
    {
        public string FullName { get; set; }

        public string Title { get; set; } = "";

        public string Birthday { get; set; } = "";

        public string Country { get; set; } = "";

        public string City { get; set; } = "";

        public string Address { get; set; } = "";

        public string PostCode { get; set; } = "";

        public string ProfilePicture { get; set; } = "";

        public ICollection<Item> ItemsSold { get; set; } = new HashSet<Item>();

        public ICollection<Bid> Bids { get; set; } = new HashSet<Bid>();
    }
}