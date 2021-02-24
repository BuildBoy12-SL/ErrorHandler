namespace ErrorHandler
{
    using UnityEngine;

    public partial class EventHandlers
    {
        public EventHandlers(Config config) => _config = config;
        private readonly Config _config;

        internal void LogCallback(string condition, string stackTrace, LogType type)
        {
            if (type != LogType.Exception)
                return;

            SendMessage(string.Empty, $"{condition}\n{stackTrace}");
        }
    }
}