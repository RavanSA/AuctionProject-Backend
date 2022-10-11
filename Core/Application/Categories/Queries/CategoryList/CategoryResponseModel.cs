namespace Application.Categories.Queries.CategoryList

{
    using System;
    using System.Collections.Generic;
    using Domain.Entities;
    using global::Common.AutoMapping.Interfaces;

    public class CategoryResponseModel : IMapWith<Category>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CategoryImage { get; set; }

    }
}