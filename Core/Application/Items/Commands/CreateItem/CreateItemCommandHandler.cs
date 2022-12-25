namespace Application.Items.Commands.CreateItem
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Interfaces;
    using Common.Models;
    using Domain.Entities;
    using MediatR;

    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Response<ItemResponseModel>>
    {
        private readonly IAuctionSystemDbContext context;
        private readonly IMapper mapper;

        public CreateItemCommandHandler(IAuctionSystemDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public async Task<Response<ItemResponseModel>> Handle(CreateItemCommand request,
            CancellationToken cancellationToken)
        {
  

            var item = this.mapper.Map<Item>(request);
            item.StartTime = item.StartTime.ToUniversalTime();
            item.EndTime = item.EndTime.ToUniversalTime();

            await this.context.Items.AddAsync(item, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            return new Response<ItemResponseModel>(new ItemResponseModel(item.Id));
        }
    }
}