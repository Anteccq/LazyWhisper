using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;

namespace LazyWhisper.Module
{
    public class SqlDataRepository : IDataRepository
    {
        private string _connectionString;

        public SqlDataRepository(IOptions<Config> config)
        {
            _connectionString = config.Value.ConnectionStrings;
        }

        public async Task<CustomCommand> FindAsync(string commandName, ulong channelId)
        {
            var sql =
                "select command, reply from commands " +
                "where guild_id = @guildId " +
                "and command = @command";

            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return await connection.QueryFirstOrDefaultAsync<CustomCommand>(sql, new { guildId = channelId, command = commandName });
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
