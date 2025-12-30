using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.DTOs;
public sealed class PushMessage
{
    public string Title { get; init; } = default!;
    public string Body { get; init; } = default!;
    public Dictionary<string, string>? Data { get; init; }
}
