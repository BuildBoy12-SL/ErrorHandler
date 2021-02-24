namespace ErrorHandler
{
    using DSharp4Webhook.Core;
    using DSharp4Webhook.Core.Constructor;
    using Exiled.API.Features;
    using System;

    public partial class EventHandlers
    {
        internal void SendMessage(string plugin, string error)
        {
            if (string.IsNullOrEmpty(error))
                return;

            Plugin.Singleton.Webhook.SendMessage(PrepareMessage(plugin, error).Build()).Queue((result, isSuccessful) =>
            {
                if (!isSuccessful)
                    Log.Warn("Sending the report failed!");
            });
        }

        private static readonly EmbedBuilder EmbedBuilder = ConstructorProvider.GetEmbedBuilder();
        private static readonly EmbedFieldBuilder FieldBuilder = ConstructorProvider.GetEmbedFieldBuilder();
        private static readonly MessageBuilder MessageBuilder = ConstructorProvider.GetMessageBuilder();

        private MessageBuilder PrepareMessage(string plugin, string error)
        {
            EmbedBuilder.Reset();
            FieldBuilder.Reset();
            MessageBuilder.Reset();

            FieldBuilder.Inline = true;

            FieldBuilder.Name = "Server Port";
            FieldBuilder.Value = Server.Port.ToString();
            EmbedBuilder.AddField(FieldBuilder.Build());

            if (!string.IsNullOrEmpty(plugin))
            {
                FieldBuilder.Name = "Plugin";
                FieldBuilder.Value = plugin;
                EmbedBuilder.AddField(FieldBuilder.Build());
            }

            FieldBuilder.Inline = false;

            FieldBuilder.Name = "Error Caught";
            FieldBuilder.Value = error;
            EmbedBuilder.AddField(FieldBuilder.Build());

            EmbedBuilder.Title = _config.WebhookTitle;
            EmbedBuilder.Timestamp = DateTimeOffset.UtcNow;

            MessageBuilder.AddEmbed(EmbedBuilder.Build());

            return MessageBuilder;
        }
    }
}