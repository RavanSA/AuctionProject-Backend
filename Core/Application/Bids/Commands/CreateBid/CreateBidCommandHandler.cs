namespace Application.Bids.Commands.CreateBid
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Models;
    using AutoMapper;
    using Common.Interfaces;
    using Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class CreateBidCommandHandler : IRequestHandler<CreateBidCommand, Result>
    {
        private readonly IAuctionSystemDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateBidCommandHandler(IAuctionSystemDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(CreateBidCommand request, CancellationToken cancellationToken)
        {

            /*
             * En yüksek teklif veren yeniden teklif vere bilmesin
             * */

            var getHighestBid = await _context.Bids.OrderByDescending(x => x.Amount).FirstOrDefaultAsync();
            if (getHighestBid is not null && getHighestBid?.UserId == _currentUserService.UserId)
            {
                    return Result.Failure("User already recorded");
            }

            /*
            * Minimum bid qeder artıra bilir
            * */


            var getLowestBid = await _context.Bids.OrderBy(x => x.Amount).FirstOrDefaultAsync();
            if (getLowestBid is not null && getLowestBid.Amount > request.Amount)
                return Result.Failure("The lowest bidder cannot bid again ");


            var bid = _mapper.Map<Bid>(request);
            await _context.Bids.AddAsync(bid, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}