using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptRunner.Tests.Data.Scripts
{
    internal class Script1 : IScript
    {
        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }

    internal class Script2 : IScript
    {
        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }

    internal class Script3 : IScript
    {
        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }

    internal class ScriptNotImplementingIScript
    {
        public Task Execute()
        {
            throw new NotImplementedException();
        }
    }

    // Pulls in scripts from multiple namespaces
    namespace Nested
    {
        internal class Script1 : IScript
        {
            public Task Execute()
            {
                throw new NotImplementedException();
            }
        }

        internal class Script4 : IScript
        {
            public Task Execute()
            {
                throw new NotImplementedException();
            }
        }

        internal class Script5 : IScript
        {
            public Task Execute()
            {
                throw new NotImplementedException();
            }
        }
    }
}
