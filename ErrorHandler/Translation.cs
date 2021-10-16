// -----------------------------------------------------------------------
// <copyright file="Translation.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ErrorHandler
{
    using Exiled.API.Interfaces;

    /// <inheritdoc />
    public class Translation : ITranslation
    {
        /// <summary>
        /// Gets or sets the translation for the webhook title.
        /// </summary>
        public string WebhookTitle { get; set; } = "Error";

        /// <summary>
        /// Gets or sets the translation for the server port title.
        /// </summary>
        public string ServerPort { get; set; } = "Server Port";

        /// <summary>
        /// Gets or sets the translation for the plugin title.
        /// </summary>
        public string Plugin { get; set; } = "Plugin";

        /// <summary>
        /// Gets or sets the translation for the error caught title.
        /// </summary>
        public string ErrorCaught { get; set; } = "Error Caught";

        /// <summary>
        /// Gets or sets the translation for the send failed warning.
        /// </summary>
        public string SendFailed { get; set; } = "Failed to send the error!";
    }
}