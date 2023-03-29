using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.SQL
{
    internal class SqlDataStore : IDataStore
    {
        public Task<List<string>> GetCompletedScriptNames()
        {
            return Task.FromResult(new List<string>()); 
        }

        public Task SetScriptAsCompleted(string name)
        {
            return Task.CompletedTask;
        }
    }
}
