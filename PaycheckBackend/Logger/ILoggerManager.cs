

namespace PaycheckBackend.Logger
{
    public interface ILoggerManager
    {
        void LogInfo(string controller, string action, string message);
        void LogInfo(string message);
        void LogWarn(string message);
        void LogDebug(string message);
        void LogError(string controller, string action, string message);
        void LogError(string message);
    }
}