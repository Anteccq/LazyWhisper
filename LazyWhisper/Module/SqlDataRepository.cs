using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public async Task<CustomCommand[]> FindAllAsync(ulong channelId)
        {
            var sql =
                "select command, reply from commands " +
                "where guild_id = @guildId ";

            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            var result = await connection.QueryAsync<CustomCommand>(sql, new {guildId = channelId});
            return result.ToArray();
        }

        public async Task DeleteAsync(string commandName, ulong channelId)
        {
            var sql =
                "delete from commands " +
                "where guild_Id = @guildId " +
                "and command = @command ";

            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            var transaction = await connection.BeginTransactionAsync();

            try
            {
                await connection.ExecuteAsync(sql, new {guildId = channelId, command = commandName});
                await transaction.CommitAsync();
            }
            catch
            {
                try
                {
                    await transaction.RollbackAsync();
                }
                catch
                {
                    // ignored
                }
            }
        }

        public async Task UpdateAsync(string commandName, string reply, ulong channelId)
        {
            var sql =
                "update commands set reply = @reply " +
                "where command = @command " +
                "and guild_id = @guildId ";

            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            var transaction = await connection.BeginTransactionAsync();
            try
            {
                await connection.ExecuteAsync(sql, new {reply, command = commandName, guildId = channelId});
                await transaction.CommitAsync();
            }
            catch
            {
                try
                {
                    await transaction.RollbackAsync();
                }
                catch
                {
                    // ignored
                }
            }
        }

        public async Task InsertAsync(string commandName, string reply, ulong channelId)
        {
            var sql =
                "insert into commands ( command, reply, guild_id ) " +
                "values ( @command , @reply , @guildId )";

            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            var transaction = await connection.BeginTransactionAsync();

            try
            {
                await connection.ExecuteAsync(sql, new {command = commandName, reply, guildId = channelId});
                await transaction.CommitAsync();
            }
            catch
            {
                try
                {
                    await transaction.RollbackAsync();
                }
                catch
                {
                    // Log を残せるようにしておきたいですね。将来的には
                }
            }
        }
    }
}
