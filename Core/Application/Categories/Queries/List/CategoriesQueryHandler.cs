namespace Application.Categories.Queries.List
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
        CategoriesQueryHandler : IRequestHandler<CategoriesQuery, MultiResponse<CategoriesResponseModel>>
    {
        private readonly IAuctionSystemDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesQueryHandler(IAuctionSystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MultiResponse<CategoriesResponseModel>> Handle(CategoriesQuery request,
            CancellationToken cancellationToken)
        {
            var categories = await _context
                .Categories
                .Include(c => c.SubCategories)
                .ProjectTo<CategoriesResponseModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new MultiResponse<CategoriesResponseModel>(categories);
        }
    }
}