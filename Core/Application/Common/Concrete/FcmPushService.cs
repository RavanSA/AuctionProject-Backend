using Application.Common.Abstraction;
using Application.Common.DTOs;
using Application.Common.Extensions;
using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Concrete;
using FirebaseAdmin.Messaging;
using FcmNotification = FirebaseAdmin.Messaging.Notification;

public sealed class FcmPushService : IFcmPushService
{
     public async Task<string> SendAsync(string token, PushMessage message)
    {
        var msg = new Message
        {
            Token = token,
            Notification = new FcmNotification
            {
                Title = message.Title,
                Body = message.Body
            },
            Data = message.Data
        };

        return await FirebaseMessaging
            .DefaultInstance
            .SendAsync(msg);
    }

    // 2️⃣ Aynı mesaj – çok token (BEST PRACTICE)
    public async Task<PushSendResult> SendBatchAsync(
        IReadOnlyCollection<string> tokens,
        PushMessage message)
    {
        var result = new PushSendResult
        {
            Total = tokens.Count
        };

        foreach (var batch in tokens.ChunkBy(500))
        {
            var multicast = new MulticastMessage
            {
                Tokens = batch,
                Notification = new FcmNotification
                {
                    Title = message.Title,
                    Body = message.Body
                },
                Data = message.Data
            };

            var response =
                await FirebaseMessaging.DefaultInstance
                    .SendEachForMulticastAsync(multicast);

            result.Success += response.SuccessCount;
            result.Failed += response.FailureCount;

            for (int i = 0; i < response.Responses.Count; i++)
            {
                if (!response.Responses[i].IsSuccess)
                {
                    result.InvalidTokens.Add(batch[i]);
                }
            }
        }

        return result;
    }

     public async Task<PushSendResult> SendPersonalizedBatchAsync(
        IReadOnlyCollection<(string Token, PushMessage Message)> items)
    {
        var result = new PushSendResult
        {
            Total = items.Count
        };

        foreach (var batch in items.ChunkBy(500))
        {
            var messages = batch.Select(x => new Message
            {
                Token = x.Token,
                Notification = new FcmNotification
                {
                    Title = x.Message.Title,
                    Body = x.Message.Body
                },
                Data = x.Message.Data
            }).ToList();

            var response =
                await FirebaseMessaging.DefaultInstance
                    .SendEachAsync(messages);

            result.Success += response.SuccessCount;
            result.Failed += response.FailureCount;

            for (int i = 0; i < response.Responses.Count; i++)
            {
                if (!response.Responses[i].IsSuccess)
                {
                    result.InvalidTokens.Add(batch.ElementAt(i).Token);
                }
            }
        }

        return result;
    }
}


