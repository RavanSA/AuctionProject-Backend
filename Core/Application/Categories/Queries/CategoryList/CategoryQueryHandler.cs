namespace Application.Categories.Queries.CategoryList
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Interfaces;
    using Common.Models;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class
        CategoryQueryHandler : IRequestHandler<CategoryListQuery, MultiResponse<CategoryResponseModel>>
    {
        private readonly IAuctionSystemDbContext context;
        private readonly IMapper mapper;

        public CategoryQueryHandler(IAuctionSystemDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<MultiResponse<CategoryResponseModel>> Handle(CategoryListQuery request,
            CancellationToken cancellationToken)
        {
            var categories = await this.context
                .Categories
                .ProjectTo<CategoryResponseModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new MultiResponse<CategoryResponseModel>(categories);
        }
    }
}