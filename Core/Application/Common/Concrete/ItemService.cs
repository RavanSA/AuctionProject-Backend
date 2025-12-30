using Application.Common.Abstraction;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Concrete;
public class ItemService : IItemService
{
    private readonly IAuctionSystemDbContext _context;
    private readonly IFcmPushService _fcmPushService;

    public ItemService(IAuctionSystemDbContext context, IFcmPushService fcmPushService)
    {
        _context = context;
        _fcmPushService = fcmPushService;
    }

    public async Task ProcessItemCreation()
    {
        var getItems = await _context.Items.Where(x => x.Status == Domain.Entities.ItemStatus.Create && x.StartTime > DateTime.Now).ToListAsync();

        if (getItems is null || getItems.Count<=0) return;

        foreach (var item in getItems)
        {
            try
            {
                //TODO: notification gonderme

                item.Status = Domain.Entities.ItemStatus.Continue;
                _context.Items.Update(item);

                var newNotification = new Domain.Entities.Notification
                {
                    Id = Guid.NewGuid(),
                    Type = Domain.Entities.NotificationType.ItemStart,
                    Title = "Auksiyan Başladı",
                    Description = $"{item.Title} ucun teklif vermek başladı.",
                    Status = Domain.Entities.Status.Sent,
                    UserId = item.UserId
                };

                await _context.Notifications.AddAsync(newNotification);

                await _context.SaveChangesAsync(System.Threading.CancellationToken.None);

                var token = await _context.Users.Where(x => x.Id == item.UserId).Select(u => u.FirebaseToken).FirstOrDefaultAsync();

                if (token != null)
                {
                    await _fcmPushService.SendAsync(
                        token,
                        new PushMessage
                        {
                            Title = newNotification.Title,
                            Body = newNotification.Description,
                            Data = new Dictionary<string, string>
                            {
                                ["itemId"] = item.Id.ToString(),
                                ["type"] = "ITEM_STARTED"
                            }
                        });
                }


            }catch(Exception e)
            {
                //log
                ;
            }

        }
    }

    public async Task ProcessItemEndTime()
    {
        var getItems = await _context.Items.Where(x => x.Status == Domain.Entities.ItemStatus.Continue && DateTime.Now > x.EndTime).ToListAsync();

        if (getItems is null || getItems.Count <= 0) return;

        foreach (var item in getItems)
        {

            var getBid = await _context.Bids.Where(x => x.ItemId == item.Id).OrderBy(x => x.Amount).FirstOrDefaultAsync();
            item.Status = Domain.Entities.ItemStatus.Stop;
            await _context.SaveChangesAsync(System.Threading.CancellationToken.None);
            //notification gonder-bid qazanana
            var getWinner =await  _context.Users.Where(x => x.Id == item.UserId).Select(x=>x.FirebaseToken).FirstOrDefaultAsync();

            if (getWinner != null)
            {

                var newNotification = new Domain.Entities.Notification()
                {
                    Id = Guid.NewGuid(),
                    Type = Domain.Entities.NotificationType.BidEnd,
                    Title = "Auksiyani Qazandin",
                    Description = $"{item.Title} sizin oldu.",
                    Status = Domain.Entities.Status.Sent,
                    UserId = item.UserId
                };

                await _fcmPushService.SendAsync(
            getWinner,
            new PushMessage
            {
                Title = newNotification.Title,
                Body =newNotification.Description,
                Data = new Dictionary<string, string>
                {
                    ["itemId"] = item.Id.ToString(),
                    ["type"] = "ITEM_WON"
                }
            });
            }

        }
    }


    public async Task ProcessContinueItems()
    {
        var getItems = await _context.Items.Where(x => x.Status == Domain.Entities.ItemStatus.Continue && x.EndTime - DateTime.Now > TimeSpan.FromMinutes(15)).ToListAsync();

        if (getItems is null) return;

        foreach (var item in getItems)
        {

            //notification bid verimis olanlar ki auksiyon quatrmagina az qalib
            var tokens = await _context.Bids.Include(x=>x.User).Where(x => x.ItemId == item.Id) .Select(x =>x.User.FirebaseToken).Distinct() .ToListAsync();
         
            if (!tokens.Any())
                continue;

            var newNotification=new Domain.Entities.Notification
            {
                Id = Guid.NewGuid(),
                Type = Domain.Entities.NotificationType.BidEnd,
                Title = "Auksiyon Bitir",
                Description = $"{item.Title} üçün son deqiqələr.",
                Status = Domain.Entities.Status.Sent,
                UserId = item.UserId
            };

            await _fcmPushService.SendBatchAsync(
         tokens,
         new PushMessage
         {
             Title =newNotification.Title,
             Body = newNotification.Description,
             Data = new Dictionary<string, string>
             {
                 ["itemId"] = item.Id.ToString(),
                 ["type"] = "ITEM_ENDING_SOON"
             }
         });
        }
    }


}