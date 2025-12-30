using Application.Common.Models;
using Application.Items.Queries.Details;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notification.Queries;
public class GetUserNotificationQuery : IRequest<PagedResponse<GetUserNotificationQueryResponse>>
{
    public string? SortBy { get; set; }
    public bool SortDesc { get; set; } = false;

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
