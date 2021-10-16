// -----------------------------------------------------------------------
// <copyright file="WebhookController.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ErrorHandler
{
    using System;
    using DSharp4Webhook.Core;
    using DSharp4Webhook.Core.Constructor;
    using Exiled.API.Features;

    /// <summary>
    /// Manages interactions with the webhook.
    /// </summary>
    public class WebhookController : IDisposable
    {
        private static readonly EmbedBuilder EmbedBuilder = ConstructorProvider.GetEmbedBuilder();
        private static readonly EmbedFieldBuilder FieldBuilder = ConstructorProvider.GetEmbedFieldBuilder();
        private static readonly MessageBuilder MessageBuilder = ConstructorProvider.GetMessageBuilder();
        private readonly Plugin plugin;
        private readonly IWebhook webhook;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookController"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="Plugin"/> class.</param>
        public WebhookController(Plugin plugin)
        {
            this.plugin = plugin;
            webhook = WebhookProvider.CreateStaticWebhook(plugin.Config.WebhookUrl);
        }

        /// <summary>
        /// Sends a message via the <see cref="webhook"/>.
        /// </summary>
        /// <param name="pluginName">The name of the plugin that threw the error.</param>
        /// <param name="error">The error that was thrown.</param>
        public void SendMessage(string pluginName, string error)
        {
            if (string.IsNullOrEmpty(error))
                return;

            webhook.SendMessage(PrepareMessage(pluginName, error).Build()).Queue((result, isSuccessful) =>
            {
                if (!isSuccessful)
                    Log.Warn(plugin.Translation.SendFailed);
            });
        }

        /// <inheritdoc />
        public void Dispose()
        {
            webhook?.Dispose();
        }

        private MessageBuilder PrepareMessage(string pluginName, string error)
        {
            EmbedBuilder.Reset();
            FieldBuilder.Reset();
            MessageBuilder.Reset();

            FieldBuilder.Inline = true;

            FieldBuilder.Name = plugin.Translation.ServerPort;
            FieldBuilder.Value = Server.Port.ToString();
            EmbedBuilder.AddField(FieldBuilder.Build());

            if (!string.IsNullOrEmpty(pluginName))
            {
                FieldBuilder.Name = plugin.Translation.Plugin;
                FieldBuilder.Value = pluginName;
                EmbedBuilder.AddField(FieldBuilder.Build());
            }

            FieldBuilder.Inline = false;

            FieldBuilder.Name = plugin.Translation.ErrorCaught;
            FieldBuilder.Value = error;
            EmbedBuilder.AddField(FieldBuilder.Build());

            EmbedBuilder.Title = plugin.Translation.WebhookTitle;
            EmbedBuilder.Timestamp = DateTimeOffset.UtcNow;

            MessageBuilder.AddEmbed(EmbedBuilder.Build());

            return MessageBuilder;
        }
    }
}