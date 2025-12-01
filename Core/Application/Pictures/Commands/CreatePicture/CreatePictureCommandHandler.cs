namespace Application.Pictures.Commands.CreatePicture
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Models;
    using Common.Interfaces;
    using Domain.Entities;
    using FluentValidation.Internal;
    using MediatR;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    public class CreatePictureCommandHandler : IRequestHandler<CreatePictureCommand, Result>
    {
        private readonly IAuctionSystemDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CreatePictureCommandHandler> _logger;
        private readonly IWebHostEnvironment _env;

        public CreatePictureCommandHandler(IAuctionSystemDbContext context, IConfiguration configuration, ILogger<CreatePictureCommandHandler> logger, IWebHostEnvironment env)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
            _env = env;
        }

        public async Task<Result> Handle(CreatePictureCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Pictures == null || request.Pictures.Count <= 0) return Result.Failure("Picture is Missing");

            var folderPath = Path.Combine(_env.ContentRootPath, _configuration["ImagePath"]);
            if (string.IsNullOrEmpty(folderPath)) return Result.Failure("Cannot read foldername");


            for (int i = 0; i < request.Pictures.Count; i++)
            {
                if (request.Pictures[i] == null) continue;
                try
                {
                    string extension = ".png";
                    if (request.Pictures[i].StartsWith("data:image/jpeg;base64,") || request.Pictures[i].StartsWith("data:image/jpg;base64,"))
                    {
                        extension = ".jpg";
                        request.Pictures[i] = request.Pictures[i].Replace("data:image/jpeg;base64,", "").Replace("data:image/jpg;base64,", "");
                    }
                    if (request.Pictures[i].StartsWith("data:image/png;base64,"))
                    {
                        extension = ".png";
                        request.Pictures[i] = request.Pictures[i].Replace("data:image/png;base64,", "");
                    }
                    if (request.Pictures[i].StartsWith("data:image/webp;base64,")) {

                        extension = ".webp";
                        request.Pictures[i] = request.Pictures[i].Replace("data:image/webp;base64,", "");
                    }

                    string fileName = request.ItemId + "-" + (i) + "-" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + extension;

                    var addedPicture = new Picture
                    {
                        ItemId = request.ItemId,
                        Url = Path.Combine(folderPath, fileName)
                    };

                    var bytes = Convert.FromBase64String(request.Pictures[i]);

                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);
                    await File.WriteAllBytesAsync(addedPicture.Url, bytes);

                    await _context.Pictures.AddAsync(addedPicture, cancellationToken);
                }
                catch (Exception e)
                {
                    _logger.LogError("While saving picture:" + e.Message);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}