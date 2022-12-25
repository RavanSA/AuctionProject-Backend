namespace Application.Items.Queries.Details
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Common.Interfaces;
    using Common.Models;
    using Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class GetUserUserInfoByIdQueryHandler : IRequestHandler<GetUserInfoById, Response<GetUserUserInfoByIdQueryResponseModel>>
    {
        private readonly IAuctionSystemDbContext context;
        private readonly IMapper mapper;


        public GetUserUserInfoByIdQueryHandler(IAuctionSystemDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Response<GetUserUserInfoByIdQueryResponseModel>> Handle(GetUserInfoById request,
            CancellationToken cancellationToken)
        {
            var users = await this.context
                 .Users
                 .ProjectTo<GetUserUserInfoByIdQueryResponseModel>(this.mapper.ConfigurationProvider)
                 .SingleOrDefaultAsync(i => i.Id == request.Id, cancellationToken);


            return new Response<GetUserUserInfoByIdQueryResponseModel>(users);
        }
    }
}
