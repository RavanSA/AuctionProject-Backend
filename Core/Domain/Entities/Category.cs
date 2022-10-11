namespace Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using Common;

    public class Category : AuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryImage { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; } = new HashSet<SubCategory>();
    }
}