namespace Application.Pictures.Queries
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Interfaces;
    using Common.Models;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class
        PictureDetailsQueryHandler : IRequestHandler<PictureDetailsQuery, MultiResponse<PictureDetailsResponseModel>>
    {
        private readonly IAuctionSystemDbContext _context;
        private readonly IMapper _mapper;

        public PictureDetailsQueryHandler(IAuctionSystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MultiResponse<PictureDetailsResponseModel>> Handle(PictureDetailsQuery request,
            CancellationToken cancellationToken)
        {
                            

            var picture = await _context
                .Pictures
                .Where(p => p.ItemId == request.Id)
                .ProjectTo<PictureDetailsResponseModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new MultiResponse<PictureDetailsResponseModel>(picture);
        }
    }
}