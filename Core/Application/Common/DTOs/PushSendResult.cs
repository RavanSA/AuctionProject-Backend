using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.DTOs;
public sealed class PushSendResult
{
    public int Total { get; set; }
    public int Success { get; set; }
    public int Failed { get; set; }
    public List<string> InvalidTokens { get; } = new();
}
