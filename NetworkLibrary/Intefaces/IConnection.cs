using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary.Intefaces
{
    public interface IConnection
    {
        Task SendMessageAsync(string message);
        Task<string> ReceiveMessageAsync();
        bool ConnectionTest();
    }
}
