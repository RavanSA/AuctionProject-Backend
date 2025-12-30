using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Pictures.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Notification.Command;
internal class FirebaseNotificationCommandHandler : IRequestHandler<FirebaseNotificationCommand, Result>
{
    private readonly IAuctionSystemDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public FirebaseNotificationCommandHandler(IAuctionSystemDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Result> Handle(FirebaseNotificationCommand request, CancellationToken cancellationToken)
    {
        var getUser =await _context.Users.FirstOrDefaultAsync(x=>x.Id==_currentUserService.UserId);
        if (getUser is null) return Result.Failure("User not found");

        getUser.FirebaseToken = request.FirebaseToken;
        _context.Users.Update(getUser);
        return Result.Success();    
    }
}
