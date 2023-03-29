using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.POC.Services
{
    internal interface IServiceBusService
    {
        Task SendMessage(string message);
    }

    internal class ServiceBusService : IServiceBusService
    {
        public Task SendMessage(string message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}
