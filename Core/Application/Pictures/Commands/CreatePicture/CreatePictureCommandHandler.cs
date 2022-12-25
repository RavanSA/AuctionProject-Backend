namespace Application.Pictures.Commands.CreatePicture
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Interfaces;
    using Domain.Entities;
    using MediatR;  

    public class CreatePictureCommandHandler : IRequestHandler<CreatePictureCommand>
    {
        private readonly IAuctionSystemDbContext _context;


        public CreatePictureCommandHandler(IAuctionSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreatePictureCommand request,
            CancellationToken cancellationToken)
        {

            foreach (var picture in request.Pictures)
            {

                var addedPicture = new Picture
                {
                    ItemId = request.ItemId,
                    Url = picture
                };

                await _context.Pictures.AddAsync(addedPicture, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}