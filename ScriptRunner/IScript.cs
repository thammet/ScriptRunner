namespace ScriptRunner
{
    public interface IScript
    {
        Task Execute();
    }

    public static class ScriptExtensions
    {
        public static string GetName(this IScript script)
        {
            return script.GetType().FullName;
        }
    }
}
