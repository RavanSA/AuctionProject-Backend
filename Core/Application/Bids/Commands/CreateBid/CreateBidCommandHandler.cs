namespace Application.Bids.Commands.CreateBid
{
    using System;
    using System.Diagnostics.CodeAnalysis;
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
             * item yarranmamisdam 24 saat erzinde
             */

            int dayCheck = 24;//databaseden oxunulacaq sonra
            var getItem=await _context.Items.Where(x=>x.Id==request.ItemId).FirstOrDefaultAsync();

            if (getItem is not null)
            {
                if (DateTime.Now - getItem.StartTime < TimeSpan.FromHours(dayCheck))
                {
                    return Result.Failure($"Cannot create bid in {dayCheck} hour after creating item ");
                }
               

            }


            /*
             * son 10 dq 
             */
            var getBids=await _context.Bids.Where(x=>x.ItemId==request.ItemId).OrderByDescending(x=>x.Created).FirstOrDefaultAsync();

            if(getBids is not null)
            {
                if (DateTime.Now-getBids.Created < TimeSpan.FromMinutes(10)) return Result.Failure("Cannot create bid in 10 minute");
            }
            /*
             * En yüksek teklif veren yeniden teklif vere bilmesin
             * */

            var getHighestBid = await _context.Bids.Where(x=>x.ItemId==request.ItemId).OrderByDescending(x => x.Amount).FirstOrDefaultAsync();
            if (getHighestBid is not null && getHighestBid?.UserId == _currentUserService.UserId)
            {
                    return Result.Failure("User already recorded");
            }

            /*
            * Minimum bid qeder artıra bilir
            * */


            var getLowestBid = await _context.Bids.Where(x => x.ItemId == request.ItemId).OrderBy(x => x.Amount).FirstOrDefaultAsync();
            if (getLowestBid is not null && getLowestBid.Amount > request.Amount)
                return Result.Failure("The lowest bidder cannot bid again ");

            


 
            var newBid = new Bid()
            {
                Amount = request.Amount,
                UserId = request.UserId,
                ItemId = request.ItemId,
                Latitude= request.Latitude,
                Longitude= request.Longitude
            };
            await _context.Bids.AddAsync(newBid, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}