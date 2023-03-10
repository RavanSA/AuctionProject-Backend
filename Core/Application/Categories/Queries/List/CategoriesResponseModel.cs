namespace Application.Categories.Queries.List
{
    using System;
    using System.Collections.Generic;
    using Domain.Entities;
    using global::Common.AutoMapping.Interfaces;

    public class CategoriesResponseModel : IMapWith<Category>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryImage { get; set; }

        public IEnumerable<CategoriesDto> SubCategories { get; set; }
    }
}