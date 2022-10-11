namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using Common;

    public class Messages : AuditableEntity
    {
        public Guid Id { get; set; }
        
        public string Message { get; set; }

        public string SellerId { get; set; }


        public Guid? ItemId { get; set; }

        public string BidderId { get; set; }

        public string MessageOwner { get; set; }

        public Item Item { get; set; }


    }
}