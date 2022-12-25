namespace Application.Items.Commands.CreateItem
{
    using FluentValidation;
    using global::Common;

    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        private readonly IDateTime dateTime;

        public CreateItemCommandValidator(IDateTime dateTime){}
    }
}