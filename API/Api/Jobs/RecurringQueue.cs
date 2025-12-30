using Application.Common.Abstraction;
using Hangfire;

namespace Api.Jobs;

public class RecurringQueue
{
    private readonly IRecurringJobManager _recurringJobManager;
    private readonly IItemService _itemService;
    public RecurringQueue( IRecurringJobManager recurringJobManager, IItemService itemService)
    {
        _recurringJobManager = recurringJobManager;
        _itemService = itemService;
    }

    public void ScheduleJobs()
    {
            _recurringJobManager.AddOrUpdate("WaitingItems",
           () => _itemService.ProcessItemCreation(),
           Cron.MinuteInterval(2));

        _recurringJobManager.AddOrUpdate("EndedItemBids",
      () => _itemService.ProcessItemEndTime(),
      Cron.MinuteInterval(2));


        _recurringJobManager.AddOrUpdate("NotifyUsersOfItemEnd",
      () => _itemService.ProcessContinueItems(),
      Cron.MinuteInterval(8));


    }


}
