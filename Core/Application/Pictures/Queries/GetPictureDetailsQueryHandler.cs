﻿namespace Application.Pictures.Queries
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Exceptions;
    using Common.Interfaces;
    using Common.Models;
    using Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class
        GetPictureDetailsQueryHandler : IRequestHandler<GetPictureDetailsQuery, MultiResponse<PictureDetailsResponseModel>>
    {
        private readonly IAuctionSystemDbContext context;
        private readonly IMapper mapper;

        public GetPictureDetailsQueryHandler(IAuctionSystemDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<MultiResponse<PictureDetailsResponseModel>> Handle(GetPictureDetailsQuery request,
            CancellationToken cancellationToken)
        {
                            

            var picture = await this.context
                .Pictures
                .Where(p => p.ItemId == request.Id)
                .ProjectTo<PictureDetailsResponseModel>(this.mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

          

            return new MultiResponse<PictureDetailsResponseModel>(picture);
        }
    }
}