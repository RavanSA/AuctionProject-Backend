namespace Application.Users.Commands.CreateUser
{
    using FluentValidation;
    using global::Common;

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator() { }
    }
}