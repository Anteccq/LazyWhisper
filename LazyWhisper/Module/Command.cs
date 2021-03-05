using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Net;

namespace LazyWhisper.Module
{
    public class Command : ModuleBase
    {
        private IDataRepository _dataRepository;

        private static readonly string[] defaultCommands = 
        {
            "add", "remove ", "edit", "list", "help"
        };

        public Command(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [Command("add")]
        public async Task AddCommandAsync(params string[] args)
        {
            if (args.Length < 2 || args[0].Contains('!'))
            {
                await ReplyAsync("!add CommandName Message");
                return;
            }

            if (defaultCommands.Contains(args[0]))
            {
                await ReplyAsync("Default commands cannot be registered");
                return;
            }

            var result = await _dataRepository.FindAsync(args[0], Context.Guild.Id);
            if (result != null)
            {
                await ReplyAsync("The command has already been registered");
                return;
            }

            var message = args.Skip(1).Aggregate((a, b) => $"{a} {b}");
            await _dataRepository.InsertAsync(args[0], message, Context.Guild.Id);
            await ReplyAsync($"The {args[0]} command has been registered");
        }

        [Command("edit")]
        public async Task EditCommandAsync(params string[] args)
        {
            if (args.Length < 2)
            {
                await ReplyAsync("!edit CommandName Message");
                return;
            }

            var result = await _dataRepository.FindAsync(args[0], Context.Guild.Id);
            if (result == null)
            {
                await ReplyAsync("The command is not registered.");
                return;
            }

            var message = args.Skip(1).Aggregate((a, b) => $"{a} {b}");
            await _dataRepository.UpdateAsync(args[0], message, Context.Guild.Id);
            await ReplyAsync($"The {args[0]} command has been changed");
        }

        [Command("remove")]
        public async Task RemoveCommandAsync(params string[] args)
        {
            if (args.Length != 1)
            {
                await ReplyAsync("!remove CommandName");
                return;
            }

            var result = await _dataRepository.FindAsync(args[0], Context.Guild.Id);
            if (result == null)
            {
                await ReplyAsync("The command is not registered.");
                return;
            }

            await _dataRepository.DeleteAsync(args[0], Context.Guild.Id);
            await ReplyAsync($"The {args[0]} command has been deleted");
        }

        [Command("list")]
        public async Task ListCommandAsync()
        {
            var commands = await _dataRepository.FindAllAsync(Context.Guild.Id);
            var isExists = commands != null && commands.Length != 0;
            var eb = new EmbedBuilder()
            {
                Color = Color.DarkBlue,
                Title = "Command List",
                Description = isExists
                    ? commands.Select(x => x.Command).Aggregate((a, b) => $"{a}\n{b}")
                    : "No command available"
            };
            await ReplyAsync(embed: eb.Build());
        }

        [Command("help")]
        public async Task HelpCommandAsync()
        {
            var footer = new EmbedFooterBuilder()
                .WithIconUrl(
                    "https://raw.githubusercontent.com/AntiquePendulum/AntiquePendulum/master/Images/AntiqueR-simple_small.png")
                .WithText("Developed by AntiqueR");
            var eb = new EmbedBuilder()
            {
                Color = Color.DarkRed,
                Title = "Default Command List",
                Footer = footer
            };
            eb.AddField("Adding", "```!add Command Message```");
            eb.AddField("Editing", "```!edit Command Message```");
            eb.AddField("Deleting", "```!remove Command```");
            eb.AddField("Command List", "```!list```");
            eb.AddField("Source Code", "[LazyWhisper GitHub](https://github.com/AntiquePendulum/LazyWhisper)");
            await ReplyAsync(embed:eb.Build());
        }
    }
}
