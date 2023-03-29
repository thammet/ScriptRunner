namespace ScriptRunner
{
    public interface IScriptExecutionService
    {
        Task ExecuteScripts();
    }

    public class ScriptExecutionService : IScriptExecutionService
    {
        private readonly IScriptRetrievalService _scriptRetrievalService;
        private readonly IDataStore _dataStore;

        public ScriptExecutionService(IScriptRetrievalService scriptRetrievalService, IDataStore dataStore)
        {
            _scriptRetrievalService = scriptRetrievalService;
            _dataStore = dataStore;
        }

        public async Task ExecuteScripts()
        {
            var scripts = await GetNewScripts();

            foreach (var script in scripts)
            {
                Console.WriteLine($"Executing '{script.GetName()}'");

                await script.Execute();

                await _dataStore.SetScriptAsCompleted(script.GetName());
            }
        }

        public async Task<IEnumerable<IScript>> GetNewScripts()
        {
            var completedScripts = await _dataStore.GetCompletedScriptNames();
            return _scriptRetrievalService.Scripts.Where(x => !completedScripts.Contains(x.GetName()));
        }
    }
}
