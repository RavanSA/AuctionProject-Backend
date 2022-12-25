namespace Application.Bids.Commands.CreateBid
{
    using FluentValidation;
    using global::Common;

    public class CreateBidCommandValidator : AbstractValidator<CreateBidCommand>
    {
        public CreateBidCommandValidator()
        {
        }
    }
}