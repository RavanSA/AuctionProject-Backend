using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Hubss
{
    public interface IMessageHubClient
    {
        Task SendMessageToUser(string message);
    }
}
