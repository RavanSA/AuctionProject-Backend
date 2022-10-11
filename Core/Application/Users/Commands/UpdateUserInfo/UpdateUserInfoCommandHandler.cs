using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Users.Commands.UpdateUserInfo
{
    public class UpdateUserInfoCommandHandler: IRequestHandler<UpdateUserInfoCommand>
    {
        private readonly IAuctionSystemDbContext context;

        public UpdateUserInfoCommandHandler(
            IAuctionSystemDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
        {

            var user = await this.context
                    .Users
                    .FindAsync(request.Id);

            user.FullName = request.FullName;
            user.PhoneNumber = request.PhoneNumber;
            user.Address = request.Address;
            user.Birthday = request.Birthday;
            user.City = request.City;
            user.Country = request.Country;
            user.PostCode = request.PostCode;
            user.ProfilePicture = request.ProfilePicture;
            user.Title = request.Title;

            this.context.Users.Update(user);

            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}
