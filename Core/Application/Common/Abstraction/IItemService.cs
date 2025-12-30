using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Abstraction;
public interface IItemService
{
    Task ProcessItemCreation();
    Task ProcessItemEndTime();
    Task ProcessContinueItems();
}
