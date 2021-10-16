﻿// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ErrorHandler
{
    using System;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using HarmonyLib;
    using UnityEngine;

    /// <summary>
    /// The main plugin class.
    /// </summary>
    public class Plugin : Plugin<Config, Translation>
    {
        private Harmony harmony;

        /// <summary>
        /// Gets the only existing instance of the <see cref="Plugin"/> class.
        /// </summary>
        public static Plugin Instance { get; private set; }

        /// <summary>
        /// Gets an instance of the <see cref="ErrorHandler.EventHandlers"/> class.
        /// </summary>
        public EventHandlers EventHandlers { get; private set; }

        /// <summary>
        /// Gets an instance of the <see cref="ErrorHandler.WebhookController"/> class.
        /// </summary>
        public WebhookController WebhookController { get; private set; }

        /// <inheritdoc />
        public override string Author { get; } = "Build";

        /// <inheritdoc />
        public override PluginPriority Priority { get; } = PluginPriority.Highest;

        /// <inheritdoc />
        public override Version RequiredExiledVersion { get; } = new Version(3, 0, 5);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            if (string.IsNullOrEmpty(Config.WebhookUrl))
            {
                Log.Error("The webhook URL must be set for this plugin to launch!");
                return;
            }

            Instance = this;

            harmony = new Harmony($"errorHandler.{DateTime.UtcNow.Ticks}");
            harmony.PatchAll();

            EventHandlers = new EventHandlers(this);
            Application.logMessageReceived += EventHandlers.LogCallback;

            WebhookController = new WebhookController(this);
            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            Application.logMessageReceived -= EventHandlers.LogCallback;
            EventHandlers = null;

            WebhookController.Dispose();
            WebhookController = null;

            harmony.UnpatchAll(harmony.Id);
            harmony = null;

            Instance = null;
            base.OnDisabled();
        }
    }
}