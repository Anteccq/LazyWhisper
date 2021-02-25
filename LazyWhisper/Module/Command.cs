using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Net;

namespace LazyWhisper.Module
{
    public class Command : ModuleBase
    {

        [Command("add")]
        public async Task AddCommandAsync(params string[] args)
        {
            if (args.Length < 2 || args[0].Contains('!'))
            {
                await ReplyAsync("!add CommandName Message");
                return;
            }
            var message = args.Skip(1).Aggregate((a, b) => $"{a} {b}");
        }
    }
}
