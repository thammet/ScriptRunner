using Microsoft.Extensions.DependencyInjection;

namespace ScriptRunner.SQL
{
    public static class ScriptRunnerOptionsExtensions
    {
        public static ScriptRunnerOptions AddSqlDataStore(this ScriptRunnerOptions scriptRunnerOptions, string connectionString)
        {
            scriptRunnerOptions.ServiceCollection.AddScoped<IDataStore, SqlDataStore>();

            return scriptRunnerOptions;
        }
    }
}