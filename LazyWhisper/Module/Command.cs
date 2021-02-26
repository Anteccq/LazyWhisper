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
        private IDataRepository _dataRepository;

        public Command(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
    }
}
