using Application.Bids.Queries.LoggedUserBids;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Items.Queries.Details;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Notification.Queries.GetUserNotification;
public class GetUserNotificationQueryHandler : IRequestHandler<GetUserNotificationQuery, PagedResponse<GetUserNotificationQueryResponse>>
{
    private readonly IAuctionSystemDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;


    public GetUserNotificationQueryHandler(IAuctionSystemDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }
    public async Task<PagedResponse<GetUserNotificationQueryResponse>> Handle(GetUserNotificationQuery request, CancellationToken cancellationToken)
    {

        var getUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId);

        if (getUser is null) return PagedResponse<GetUserNotificationQueryResponse>.Failure("User not found");



        var queryable = _context.Notifications.AsNoTracking().Where(x => x.UserId == getUser.Id);



        var totalItemsCount = await queryable.CountAsync(cancellationToken);

        var skip = (request.Page - 1) * request.PageSize;

        var list = await queryable
            .Skip(skip)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var notification = list
            .Select(_mapper.Map<GetUserNotificationQueryResponse>)
            .ToList();



        var result = PaginationHelper.CreatePaginatedBidResponse(notification, totalItemsCount);

        return result;
    }
}
