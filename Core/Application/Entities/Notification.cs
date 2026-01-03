using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;//domain??-layer yaradilacaq
public class Notification : AuditableEntity
{
    public Guid Id { get; set; }
    public NotificationType Type { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public string UserId { get; set; }

    
}

public enum NotificationType
{
    ItemStart = 0,
    NewBid = 1,
    BidEnd = 2,
    Generic=3
}

public enum Status
{
    Create = 0,
    Sent = 1,
    Failed = 2
}
