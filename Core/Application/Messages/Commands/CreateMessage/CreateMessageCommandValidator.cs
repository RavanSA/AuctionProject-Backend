namespace Application.Messages.Commands.CreateMessage
{
    using FluentValidation;
    using global::Common;

    public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageCommandValidator()
        {
            this.RuleFor(p => p.Message).NotEmpty();
              
        }
    }
}