// -----------------------------------------------------------------------
// <copyright file="EventHandlers.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ErrorHandler
{
    using UnityEngine;

    /// <summary>
    /// Handles all required events.
    /// </summary>
    public class EventHandlers
    {
        private readonly Plugin plugin;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventHandlers"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="Plugin"/> class.</param>
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        /// <inheritdoc cref="Application.logMessageReceived"/>
        public void LogCallback(string condition, string stackTrace, LogType type)
        {
            if (type == LogType.Exception)
                plugin.WebhookController.SendMessage(string.Empty, $"{condition}\n{stackTrace}");
        }
    }
}