using Application.Common.Abstraction;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Notification.Command.FirebaseNotification;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Notification.Command.SendNotification;
public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand, Result>
{
    private readonly IAuctionSystemDbContext _context;
    //private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<SendNotificationCommandHandler> _logger;
    private readonly IFcmPushService _fcmPushService;
    public SendNotificationCommandHandler(IAuctionSystemDbContext context, ILogger<SendNotificationCommandHandler> logger, IFcmPushService fcmPushService)
    {
        _context = context;
        _logger = logger;
        _fcmPushService = fcmPushService;
    }


    public async Task<Result> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        var getUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
        if (getUser is null) return Result.Failure("User not found");

     if(string.IsNullOrEmpty(getUser.FirebaseToken))
        {
            return Result.Failure("Firebase token not found for user");
        }


     var notification = new Domain.Entities.Notification
     {
         UserId = getUser.Id,
         Title = request.Title,
         Description = request.Body,
         Type = NotificationType.Generic,
         Status =  Status.Sent,
     };
        await _context.Notifications.AddAsync(notification);
        await _context.SaveChangesAsync(cancellationToken);

        await _fcmPushService.SendAsync(getUser.FirebaseToken, new Common.DTOs.PushMessage()
     {
         Body = request.Body,
         Title = request.Title,
           Data = new Dictionary<string, string>
           {
               ["type"] = "GENERIC_NOTIFICATION"
           }
     });



        _logger.LogInformation("Firebase token updated for user", getUser.Id);
        return Result.Success();
    }
}
