using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ScriptRunner
{
    public class ScriptRunnerOptions
    {
        public IServiceCollection ServiceCollection { get; init; }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddScriptRunner(this IServiceCollection serviceCollection, Assembly assembly, Action<ScriptRunnerOptions> options)
        {
            var scriptTypes = GetScriptTypes(assembly);

            foreach(var scriptType in scriptTypes)
            {
                serviceCollection.AddScoped(scriptType);
            }

            serviceCollection.AddSingleton<IScriptRetrievalService>(provider =>
            {
                var scripts = scriptTypes.Select(x => (IScript) provider.GetRequiredService(x));

                return new ScriptService(scripts);
            });

            serviceCollection.AddSingleton<IScriptExecutionService, ScriptExecutionService>();

            options(new ScriptRunnerOptions
            {
                ServiceCollection = serviceCollection
            });

            return serviceCollection;
        }

        public static IEnumerable<Type> GetScriptTypes(Assembly assembly)
        {
            return assembly.GetTypes().Where(p => p.IsClass && typeof(IScript).IsAssignableFrom(p));
        }
    }
}
