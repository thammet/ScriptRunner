using Microsoft.Extensions.DependencyInjection;
using ScriptRunner;
using ScriptRunner.POC.Services;
using ScriptRunner.SQL;
using System.Reflection;

var serviceCollection = new ServiceCollection();

serviceCollection
    // Register script dependencies
    .AddScoped<IServiceBusService, ServiceBusService>()

    // Register scripts
    .AddScriptRunner(Assembly.GetExecutingAssembly(), options =>
    {
        options.AddSqlDataStore("");
    });

var serviceProvider = serviceCollection.BuildServiceProvider();

var scriptExecutionService = serviceProvider.GetService<IScriptExecutionService>();

scriptExecutionService.ExecuteScripts();
