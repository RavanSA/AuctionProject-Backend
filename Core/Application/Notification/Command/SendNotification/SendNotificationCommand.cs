using Application.Common.Models;
using Common.AutoMapping.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notification.Command.SendNotification;
public class SendNotificationCommand : IRequest<Result>
{
    public string Title { get; set; }
    public string Body { get; set; }
    public string UserId { get; set; }
}
