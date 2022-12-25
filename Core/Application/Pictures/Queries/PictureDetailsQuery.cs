namespace Application.Pictures.Queries
{
    using System;
    using Common.Models;
    using MediatR;

    public class PictureDetailsQuery : IRequest<MultiResponse<PictureDetailsResponseModel>>
    {
        public PictureDetailsQuery(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}