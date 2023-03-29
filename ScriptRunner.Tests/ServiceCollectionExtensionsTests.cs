using Microsoft.Extensions.DependencyInjection;
using ScriptRunner.Tests.Data.Scripts;
using ScriptRunner.Tests.Data.Scripts.Nested;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace ScriptRunner.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void Adds_Script_Retrieval_Service_Dependency()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScriptRunner(Assembly.GetExecutingAssembly(), _ => { });

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var scriptRetrievalService = serviceProvider.GetRequiredService<IScriptRetrievalService>();
        }

        [Fact]
        public void GetScriptTypes()
        {
            var scripts = ServiceCollectionExtensions.GetScriptTypes(Assembly.GetExecutingAssembly()).ToList();

            Assert.Contains(typeof(Data.Scripts.Nested.Script1), scripts);
            Assert.Contains(typeof(Data.Scripts.Script1), scripts);
            Assert.Contains(typeof(Script2), scripts);
            Assert.Contains(typeof(Script3), scripts);
            Assert.Contains(typeof(Script4), scripts);
            Assert.Contains(typeof(Script5), scripts);
            Assert.DoesNotContain(typeof(ScriptNotImplementingIScript), scripts);
            Assert.DoesNotContain(typeof(IScript), scripts);
        }
    }
}
