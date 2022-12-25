namespace Application.Bids.Commands.CreateBid
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Interfaces;
    using Domain.Entities;
    using MediatR;

    public class CreateBidCommandHandler : IRequestHandler<CreateBidCommand>
    {
        private readonly IAuctionSystemDbContext _context;
        private readonly IMapper _mapper;

        public CreateBidCommandHandler(IAuctionSystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateBidCommand request, CancellationToken cancellationToken)
        {

            var bid = _mapper.Map<Bid>(request);
            await _context.Bids.AddAsync(bid, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}