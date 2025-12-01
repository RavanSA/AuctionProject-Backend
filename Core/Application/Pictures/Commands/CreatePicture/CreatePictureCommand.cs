namespace Application.Pictures.Commands.CreatePicture
{
    using System;
    using System.Collections.Generic;
    using Common.Models;
    using Domain.Entities;
    using global::Common.AutoMapping.Interfaces;
    using MediatR;
    using Microsoft.AspNetCore.Http;

    public class CreatePictureCommand : IRequest<Result>, IMapWith<Picture>
    {
        public Guid ItemId { get; set; }

        public List<string> Pictures { get; set; } 
    }
}