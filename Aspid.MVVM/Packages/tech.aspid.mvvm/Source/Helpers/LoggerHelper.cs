using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides utility methods for formatting log messages with color-coded type information.
    /// </summary>
    internal static class LoggerHelper
    {
        /// <summary>
        /// Returns a formatted message string with path-style color coding.
        /// </summary>
        /// <param name="message">The path text to format.</param>
        /// <returns>A color-coded message string in the Unity Editor; otherwise, the original string.</returns>
        internal static string GetPathMessage(this string message) =>
            message.GetMessage("D69D85");

        /// <summary>
        /// Returns a formatted message string with class-style color coding for the specified type.
        /// </summary>
        /// <param name="type">The type whose name to format.</param>
        /// <returns>A color-coded message string in the Unity Editor; otherwise, the type name.</returns>
        internal static string GetClassMessage(this Type type) =>
            type.ToString().GetMessage("4EC9B0");

        /// <summary>
        /// Returns a formatted message string with class-style color coding.
        /// </summary>
        /// <param name="message">The text to format.</param>
        /// <returns>A color-coded message string in the Unity Editor; otherwise, the original string.</returns>
        internal static string GetClassMessage(this string message) =>
            message.GetMessage("4EC9B0");

        /// <summary>
        /// Returns a formatted message string with struct-style color coding for the specified type.
        /// </summary>
        /// <param name="type">The type whose name to format.</param>
        /// <returns>A color-coded message string in the Unity Editor; otherwise, the type name.</returns>
        internal static string GetStructMessage(this Type type) =>
            type.ToString().GetMessage("86C691");

        /// <summary>
        /// Returns a formatted message string with struct-style color coding.
        /// </summary>
        /// <param name="message">The text to format.</param>
        /// <returns>A color-coded message string in the Unity Editor; otherwise, the original string.</returns>
        internal static string GetStructMessage(this string message) =>
            message.GetMessage("86C691");

        /// <summary>
        /// Returns a formatted message string with interface-style color coding for the specified type.
        /// </summary>
        /// <param name="type">The type whose name to format.</param>
        /// <returns>A color-coded message string in the Unity Editor; otherwise, the type name.</returns>
        internal static string GetInterfaceMessage(this Type type) =>
            type.ToString().GetMessage("B8D7A3");

        /// <summary>
        /// Returns a formatted message string with interface-style color coding.
        /// </summary>
        /// <param name="message">The text to format.</param>
        /// <returns>A color-coded message string in the Unity Editor; otherwise, the original string.</returns>
        internal static string GetInterfaceMessage(this string message) =>
            message.GetMessage("B8D7A3");

        /// <summary>
        /// Wraps the message in a Unity rich-text color tag when running in the Unity Editor.
        /// </summary>
        /// <param name="message">The text to wrap.</param>
        /// <param name="color">The hex color code (without the <c>#</c> prefix).</param>
        /// <returns>A color-tagged message string in the Unity Editor; otherwise, the original string.</returns>
        private static string GetMessage(this string message, string color)
        {
#if UNITY_EDITOR
            return $"<color=#{color}>{message}</color>";
#endif
            return message;
        }
    }
}
