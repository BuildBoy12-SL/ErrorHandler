namespace ErrorHandler
{
    using Exiled.API.Interfaces;

    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public string WebhookTitle { get; set; } = "Error";
        public string WebhookUrl { get; set; } = "";
    }
}