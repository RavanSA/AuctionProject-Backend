namespace Application.Items.Commands.UpdateItem
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.ModelBinding;
    using Common.Interfaces;
    using MediatR;
    using Microsoft.EntityFrameworkCore;

    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IAuctionSystemDbContext context;

        public UpdateItemCommandHandler(
            IAuctionSystemDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {

            var test2 = request.MainItemPicture;

            var item = await this.context
                    .Items
                    .SingleOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            item.MainItemPicture = request.MainItemPicture;
            

            this.context.Items.Update(item);

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}