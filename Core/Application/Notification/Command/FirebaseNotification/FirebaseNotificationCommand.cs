using Application.Common.Models;
using Common.AutoMapping.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Notification.Command.FirebaseNotification;
public class FirebaseNotificationCommand : IRequest<Result>, IMapWith<Domain.Entities.Notification>
{
    public string FirebaseToken { get; set; }

}
