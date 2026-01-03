using Application.Common.Models;
using Common.AutoMapping.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notification.Queries.GetUserNotification;
public class GetUserNotificationQueryResponse : IMapWith<Domain.Entities.Notification>
{
    public Guid Id { get; set; }
    public NotificationType Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public string UserId { get; set; }


}
