using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Rest;
using Discord.WebSocket;

namespace LazyWhisper.Module
{
    public class CustomCommandModule
    {
        private readonly IDataRepository _dataRepository;

        public CustomCommandModule(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public async Task ExecuteAsync(string command, ulong guildId, Func<string, Task<RestUserMessage>> replyAsync)
        {
            var result = await _dataRepository.FindAsync(command, guildId);
            if(result is null) return;
            await replyAsync($"{result.Reply}");
        }
    }
}
