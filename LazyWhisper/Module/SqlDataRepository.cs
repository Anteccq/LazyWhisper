﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LazyWhisper.Module
{
    public class SqlDataRepository : IDataRepository
    {
        public Task<CustomCommand> FindAsync(string commandName, ulong channelId)
        {
            throw new NotImplementedException();
        }

        public Task<CustomCommand[]> FindAllAsync(ulong channelId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string commandName, ulong channelId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string commandName, string reply, ulong channelId)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(string commandName, string reply, ulong channelId)
        {
            throw new NotImplementedException();
        }
    }
}