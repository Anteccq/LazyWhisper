using System;
using System.Collections.Generic;
using System.Text;
using ConsoleAppFramework;
using Microsoft.Extensions.Options;

namespace LazyWhisper
{
    public class LazyWhisper : ConsoleAppBase
    {
        private IOptions<Config> _config;

        public LazyWhisper(IOptions<Config> config) => _config = config;
    }
}
