using ScriptRunner.POC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.POC.Scripts
{
    internal class Script2 : IScript
    {
        private readonly IServiceBusService _serviceBusService;

        public Script2(IServiceBusService serviceBusService)
        {
            _serviceBusService = serviceBusService;
        }

        public async Task Execute()
        {
            await _serviceBusService.SendMessage("I am script 2 sending a service bus message");
        }
    }
}
