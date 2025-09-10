using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    internal static class LoggerHelper
    {
        internal static string GetPathMessage(this string message) =>
            message.GetMessage("D69D85");
        
        internal static string GetClassMessage(this Type type) =>
            type.ToString().GetMessage("4EC9B0");
        
        internal static string GetClassMessage(this string message) =>
            message.GetMessage("4EC9B0");

        internal static string GetInterfaceMessage(this Type type) =>
            type.ToString().GetMessage("B8D7A3");
        
        internal static string GetInterfaceMessage(this string message) =>
            message.GetMessage("B8D7A3");

        private static string GetMessage(this string message, string color)
        {
#if UNITY_2022_1_OR_NEWER
            if (UnityEngine.Application.isEditor)
                return $"<color=#{color}>{message}</color>";
#endif

            return message;
        }
    }
}