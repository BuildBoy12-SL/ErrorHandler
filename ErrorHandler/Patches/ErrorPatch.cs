// -----------------------------------------------------------------------
// <copyright file="ErrorPatch.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ErrorHandler.Patches
{
#pragma warning disable SA1402
    using System.Reflection;
    using Exiled.API.Features;
    using HarmonyLib;

    /// <summary>
    /// Patches <see cref="Log.Error(object)"/> to implement <see cref="WebhookController.SendMessage"/>.
    /// </summary>
    [HarmonyPatch(typeof(Log), nameof(Log.Error))]
    internal static class ErrorPatch
    {
        private static void Postfix(string message)
        {
            Plugin.Instance.WebhookController.SendMessage(Assembly.GetCallingAssembly().GetName().Name, $"```{message}```");
        }
    }

    /// <summary>
    /// Patches <see cref="Log.Error(string)"/> to implement <see cref="WebhookController.SendMessage"/>.
    /// </summary>
    [HarmonyPatch(typeof(Log), nameof(Log.Error), typeof(string))]
    internal static class ErrorStringPatch
    {
        private static void Postfix(string message)
        {
            Plugin.Instance.WebhookController.SendMessage(Assembly.GetCallingAssembly().GetName().Name, $"```{message}```");
        }
    }
}