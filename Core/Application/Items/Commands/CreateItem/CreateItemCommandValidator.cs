namespace Application.Items.Commands.CreateItem
{
    using FluentValidation;
    using global::Common;

    public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
    {
        public CreateItemCommandValidator(IDateTime dateTime)
        {
            // Validator rules can be added here if needed
        }
    }
}