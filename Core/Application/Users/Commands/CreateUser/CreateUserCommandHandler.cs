namespace Application.Users.Commands.CreateUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Interfaces;
    using MediatR;

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserManager _userManager;

        public CreateUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userManager.CreateUserAsync(request.Email, request.Password, request.FullName);
            if (!result.Succeeded)
            {
                //TODO throw exception;
            }

            return Unit.Value;
        }
    }
}