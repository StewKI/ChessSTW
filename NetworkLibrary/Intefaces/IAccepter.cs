using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary.Intefaces
{
    public interface IAccepter
    {
        Task<bool> StartAsync();
        Task<IConnection?> AcceptAsync();
    }
}
