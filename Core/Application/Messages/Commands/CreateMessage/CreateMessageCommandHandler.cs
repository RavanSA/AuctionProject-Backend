namespace Application.Messages.Commands.CreateMessage
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Common.Interfaces;
    using Domain.Entities;
    using global::Common;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand>
    {
        private readonly IAuctionSystemDbContext context;
        private readonly IMapper mapper;

        public CreateMessageCommandHandler(IAuctionSystemDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {

            var messages = this.mapper.Map<Messages>(request);
            await this.context.Messages.AddAsync(messages, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }


    }
}