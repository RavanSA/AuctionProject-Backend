namespace Application.Users.Commands.LoginUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Interfaces;
    using Common.Models;
    using Jwt;
    using MediatR;

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Response<AuthSuccessResponse>>
    {
        private readonly IMediator _mediator;
        private readonly IUserManager _userManager;

        public LoginUserCommandHandler(IUserManager userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task<Response<AuthSuccessResponse>> Handle(LoginUserCommand request,
            CancellationToken cancellationToken)
        {

            var (result, userId) = await _userManager.SignIn(request.Email, request.Password);

   
            var model = await _mediator
                .Send(new GenerateJwtTokenCommand(userId, request.Email), cancellationToken);
            return new Response<AuthSuccessResponse>(model);
        }
    }
}