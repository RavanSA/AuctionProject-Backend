namespace Application.Categories.Queries.CategoryList
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Categories.Queries.List;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Interfaces;
    using Common.Models;
    using Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class
        CategoryHandler : IRequestHandler<CategoryQuery, MultiResponse<CategoryResponseModel>>
    {
        private readonly IAuctionSystemDbContext _context;
        private readonly IMapper _mapper;

        public CategoryHandler(IAuctionSystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MultiResponse<CategoryResponseModel>> Handle(CategoryQuery request,
            CancellationToken cancellationToken)
        {
            var categories = await _context.Categories
      .Include(c => c.SubCategories)
      .Select(x => new CategoryResponseModel
      {
          Id = x.Id,
          Name = x.Name,
          Description = x.Description,
          CategoryImage = x.CategoryImage,
          SubCategories = x.SubCategories
              .Select(sc => new CategoriesDto
              {
                  Id = sc.Id,
                  Name = sc.Name
              })
              .ToList()
      })
      .ToListAsync(cancellationToken);


            return new MultiResponse<CategoryResponseModel>(categories);
        }
    }
}