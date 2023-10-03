using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary
{
    public class Message
    {
        public string text { get; private set; }
        public Player player { get; private set; }

        public Message(string text, Player player)
        {
            this.text = text;
            this.player = player;
        }
    }
}
