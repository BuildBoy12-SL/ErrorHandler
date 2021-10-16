// -----------------------------------------------------------------------
// <copyright file="ErrorPatch.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ErrorHandler.Patches
{
    using System.Reflection;
    using Exiled.API.Features;
    using HarmonyLib;

    /// <summary>
    /// Patches <see cref="Log.Error"/> to implement <see cref="WebhookController.SendMessage"/>.
    /// </summary>
    [HarmonyPatch(typeof(Log), nameof(Log.Error))]
    internal static class ErrorPatch
    {
        private static void Postfix(object message)
        {
            Plugin.Instance.WebhookController.SendMessage(Assembly.GetCallingAssembly().GetName().Name, $"```{message}```");
        }
    }
}