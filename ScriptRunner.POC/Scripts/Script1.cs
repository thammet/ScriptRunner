using ScriptRunner.POC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.POC.Scripts
{
    internal class Script1 : IScript
    {
        private readonly IServiceBusService _serviceBusService;

        public Script1(IServiceBusService serviceBusService)
        {
            _serviceBusService = serviceBusService;
        }

        public async Task Execute()
        {
            await _serviceBusService.SendMessage("I am script 1 sending a service bus message");
        }
    }
}
