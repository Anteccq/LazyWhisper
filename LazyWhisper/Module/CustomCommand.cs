using System;
using System.Collections.Generic;
using System.Text;

namespace LazyWhisper.Module
{
    public class CustomCommand
    {
        public string Command { get; set; }
        public string Reply { get; set; }

        public CustomCommand(string command, string reply)
        {
            Command = command;
            Reply = reply;
        }
    }
}
