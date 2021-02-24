namespace ErrorHandler
{
    using DSharp4Webhook.Core;
    using Exiled.API.Enums;
    using Exiled.API.Features;
    using HarmonyLib;
    using System;
    using UnityEngine;

    public class Plugin : Plugin<Config>
    {
        internal static Plugin Singleton;

        internal EventHandlers EventHandlers;

        internal IWebhook Webhook;
        
        private Harmony _harmony;

        public override void OnEnabled()
        {
            if (string.IsNullOrEmpty(Config.WebhookUrl))
            {
                Log.Error("The webhook URL must be set for this plugin to launch!");
                return;
            }

            Singleton = this;
            EventHandlers = new EventHandlers(Config);
            Application.logMessageReceived += EventHandlers.LogCallback;

            _harmony = new Harmony($"exceptionhandler.build.exiled.{DateTime.UtcNow.Ticks}");
            _harmony.PatchAll();

            Webhook = WebhookProvider.CreateStaticWebhook(Config.WebhookUrl);

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Application.logMessageReceived -= EventHandlers.LogCallback;
            EventHandlers = null;

            Webhook?.Dispose();

            _harmony.UnpatchAll(_harmony.Id);

            Singleton = null;
            base.OnDisabled();
        }

        public override string Author { get; } = "Build";
        public override PluginPriority Priority { get; } = PluginPriority.Highest;
        public override Version RequiredExiledVersion { get; } = new Version(2, 3, 4);
        public override Version Version { get; } = new Version(1, 0, 0);
    }
}