using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Commands.UpdateItemPicture
{
   public class UpdateItemPictureCommandHandler: IRequest<UpdateItemPictureCommand>
    {
        private readonly IAuctionSystemDbContext context;


        public UpdateItemPictureCommandHandler(IAuctionSystemDbContext context)
        {
            this.context = context;

        }


        public async Task<Unit> Handle(UpdateItemPictureCommand request, CancellationToken cancellationToken)
        {
            var item = await this.context
                .Items
                .SingleOrDefaultAsync(i => i.Id == request.ItemId, cancellationToken);

            item.MainItemPicture = request.MainItemPicture;

            this.context.Items.Update(item);
            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }


    }
}
