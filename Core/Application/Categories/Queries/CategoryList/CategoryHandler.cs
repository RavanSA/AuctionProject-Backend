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
            var categories = await _context
                .Categories
                .ProjectTo<CategoryResponseModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new MultiResponse<CategoryResponseModel>(categories);
        }
    }
}