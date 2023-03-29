namespace ScriptRunner
{
    public interface IDataStore
    {
        Task<List<string>> GetCompletedScriptNames();
        Task SetScriptAsCompleted(string name);
    }
}
