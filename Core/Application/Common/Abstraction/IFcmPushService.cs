using Application.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Abstraction;
public interface IFcmPushService
{
    Task<string> SendAsync(string token, PushMessage message);

    Task<PushSendResult> SendBatchAsync(
        IReadOnlyCollection<string> tokens,
        PushMessage message);

    Task<PushSendResult> SendPersonalizedBatchAsync(
        IReadOnlyCollection<(string Token, PushMessage Message)> items);
}
