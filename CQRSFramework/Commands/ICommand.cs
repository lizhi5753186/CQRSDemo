using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSFramework.Commands
{
    public interface ICommand
    {
        Guid ID { get; }
    }
}
