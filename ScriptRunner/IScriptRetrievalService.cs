namespace ScriptRunner
{
    public interface IScriptRetrievalService
    {
        IEnumerable<IScript> Scripts { get; }
    }

    public class ScriptService : IScriptRetrievalService
    {
        public IEnumerable<IScript> Scripts { get; init; }

        public ScriptService(IEnumerable<IScript> scripts)
        {
            Scripts = scripts;
        }
    }
}
