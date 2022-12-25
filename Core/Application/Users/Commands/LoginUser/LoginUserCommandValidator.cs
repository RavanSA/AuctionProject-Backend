namespace Application.Users.Commands.LoginUser
{
    using FluentValidation;
    using global::Common;

    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator(){}
    }
}