using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LazyWhisper.Module
{
    public interface IDataRepository
    {
        Task<CustomCommand> FindAsync(string commandName, ulong channelId);
        Task<CustomCommand[]> FindAllAsync(ulong channelId);
        Task DeleteAsync(string commandName, ulong channelId);
        Task UpdateAsync(string commandName, string reply, ulong channelId);
        Task InsertAsync(string commandName, string reply, ulong channelId);
    }
}
