namespace Application.Pictures.Commands.CreatePicture
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AppSettingsModels;
    using AutoMapper;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Common.Exceptions;
    using Common.Interfaces;
    using Common.Models;
    using Domain.Entities;
    using MediatR;  
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    public class CreatePictureCommandHandler : IRequestHandler<CreatePictureCommand>
    {
        private readonly IAuctionSystemDbContext context;
        private readonly IMapper mapper;


        public CreatePictureCommandHandler(
            IAuctionSystemDbContext context,
            IMapper mapper
            )
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CreatePictureCommand request,
            CancellationToken cancellationToken)
        {

            foreach (var picture in request.Pictures)
            {

                var picturess = new Picture
                {
                    ItemId = request.ItemId,
                    Url = picture
                };

                // add that new Provider_Status object to your dbModel
                await this.context.Pictures.AddAsync(picturess, cancellationToken);
            }



            await this.context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}