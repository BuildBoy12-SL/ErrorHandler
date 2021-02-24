namespace ErrorHandler
{
    using Exiled.API.Features;
    using HarmonyLib;
    using System.Reflection;

    [HarmonyPatch(typeof(Log), nameof(Log.Error))]
    internal static class ErrorPatch
    {
        private static void Postfix(object message)
        {
            Plugin.Singleton.EventHandlers.SendMessage(Assembly.GetCallingAssembly().GetName().Name, $"```{message}```");
        }
    }
}