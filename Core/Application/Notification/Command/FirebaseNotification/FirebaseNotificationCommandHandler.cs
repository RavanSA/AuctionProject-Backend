using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Pictures.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Notification.Command.FirebaseNotification;
public class FirebaseNotificationCommandHandler : IRequestHandler<FirebaseNotificationCommand, Result>
{
    private readonly IAuctionSystemDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<FirebaseNotificationCommandHandler> _logger;
    public FirebaseNotificationCommandHandler(IAuctionSystemDbContext context, ICurrentUserService currentUserService, ILogger<FirebaseNotificationCommandHandler> logger)
    {
        _context = context;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public async Task<Result> Handle(FirebaseNotificationCommand request, CancellationToken cancellationToken)
    {
        var getUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId);
        if (getUser is null) return Result.Failure("User not found");

        getUser.FirebaseToken = request.FirebaseToken;
        _context.Users.Update(getUser);

        _logger.LogInformation("Firebase token updated for user", getUser.Id);
        return Result.Success();
    }
}
