using ILogger = NLog.ILogger;
using NLog;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PaycheckBackend.Logger
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public void LogDebug(string message) => logger.Debug(message);
        public void LogError(string message) => logger.Error(message);
        public void LogError(string controller, string action, string message) => logger.Error($"{controller}.{action}--> {message}");
        public void LogInfo(string message) => logger.Info(message);
        public void LogInfo(string controller, string action, string message) => logger.Info($"{controller}.{action}--> {message}");
        public void LogWarn(string message) => logger.Warn(message);
    }
}