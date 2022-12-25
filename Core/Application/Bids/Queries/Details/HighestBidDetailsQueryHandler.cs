namespace Application.Bids.Queries.Details
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

    public class HighestBidDetailsQueryHandler : IRequestHandler<HighestBidDetailsQuery,
        Response<HighestBidDetailsResponseModel>>
    {
        private readonly IAuctionSystemDbContext _context;
        private readonly IMapper _mapper;

        public HighestBidDetailsQueryHandler(IAuctionSystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<HighestBidDetailsResponseModel>> Handle(HighestBidDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var bid = await _context
                .Bids
                .Where(b => b.ItemId == request.ItemId)
                .ProjectTo<HighestBidDetailsResponseModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return new Response<HighestBidDetailsResponseModel>(bid);
        }
    }
}