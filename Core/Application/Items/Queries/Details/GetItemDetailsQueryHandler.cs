namespace Application.Items.Queries.Details
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Interfaces;
    using Common.Models;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetItemDetailsQueryHandler : IRequestHandler<GetItemDetailsQuery, Response<ItemDetailsResponseModel>>

    {
        private readonly IAuctionSystemDbContext _context;
        private readonly IMapper _mapper;

        public GetItemDetailsQueryHandler(IAuctionSystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<ItemDetailsResponseModel>> Handle(GetItemDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var item = await _context
                .Items
                .ProjectTo<ItemDetailsResponseModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            return new Response<ItemDetailsResponseModel>(item);
        }
    }
}