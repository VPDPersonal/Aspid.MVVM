using System;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes converter messages in one shape shared by all converters.
    /// </summary>
    /// <remarks>
    /// The <see cref="Type"/> overloads are for helpers reporting on another converter's behalf.
    /// </remarks>
    public static class ConverterLogger
    {
        private const string Prefix = "[Aspid.MVVM] ";

        /// <summary>
        /// Logs an informational message that is not an error.
        /// </summary>
        /// <param name="converter">The logging converter; a scene or asset object is pinged by default.</param>
        /// <param name="message">The message, as full sentences.</param>
        /// <param name="context">The object to ping instead of the converter.</param>
        [HideInCallstack]
        public static void Log(this IConverter converter, string message, Object? context = null) =>
            Log(converter.GetType(), message, context ?? converter as Object);

        /// <summary>
        /// Logs an informational message on behalf of the specified converter type.
        /// </summary>
        /// <param name="converterType">The logging converter's type.</param>
        /// <param name="message">The message, as full sentences.</param>
        /// <param name="context">The object to ping, when one is known.</param>
        [HideInCallstack]
        public static void Log(Type converterType, string message, Object? context = null)
        {
            var converterName = ConverterMessageText.GetTypeName(converterType);
            Debug.Log($"{Prefix}{converterName}: {message}", context);
        }

        /// <summary>
        /// Reports a problem: a value that would not convert, a bad setting, an impossible reverse conversion.
        /// </summary>
        /// <param name="converter">The reporting converter; a scene or asset object is pinged by default.</param>
        /// <param name="problem">What is wrong, as a sentence without the trailing period.</param>
        /// <param name="consequence">What the converter does instead, as a full sentence.</param>
        /// <param name="context">The object to ping instead of the converter.</param>
        [HideInCallstack]
        public static void LogError(this IConverter converter, string problem, string consequence, Object? context = null) =>
            LogError(converter.GetType(), problem, consequence, context ?? converter as Object);

        /// <summary>
        /// Reports a problem on behalf of the specified converter type.
        /// </summary>
        /// <param name="converterType">The reporting converter's type.</param>
        /// <param name="problem">What is wrong, as a sentence without the trailing period.</param>
        /// <param name="consequence">What the converter does instead, as a full sentence.</param>
        /// <param name="context">The object to ping, when one is known.</param>
        [HideInCallstack]
        public static void LogError(Type converterType, string problem, string consequence, Object? context = null)
        {
            var converterName = ConverterMessageText.GetTypeName(converterType);
            Debug.LogError($"{Prefix}{converterName}: {problem}. {consequence}", context);
        }

        /// <summary>
        /// Reports an exception the converter caught.
        /// </summary>
        /// <param name="converter">The throwing converter; a scene or asset object is pinged by default.</param>
        /// <param name="exception">The exception caught.</param>
        /// <param name="consequence">What the converter does instead, as a full sentence.</param>
        /// <param name="context">The object to ping instead of the converter.</param>
        [HideInCallstack]
        public static void LogError(this IConverter converter, Exception exception, string consequence, Object? context = null) =>
            LogError(converter.GetType(), exception, consequence, context ?? converter as Object);

        /// <summary>
        /// Reports an exception on behalf of the specified converter type.
        /// </summary>
        /// <param name="converterType">The throwing converter's type.</param>
        /// <param name="exception">The exception caught.</param>
        /// <param name="consequence">What the converter does instead, as a full sentence.</param>
        /// <param name="context">The object to ping, when one is known.</param>
        [HideInCallstack]
        public static void LogError(Type converterType, Exception exception, string consequence, Object? context = null)
        {
            var converterName = ConverterMessageText.GetTypeName(converterType);
            var exceptionName = ConverterMessageText.GetTypeName(exception.GetType());

            Debug.LogError(
                message: $"{Prefix}{converterName}: threw {exceptionName} ({exception.Message}). {consequence}\n{exception}",
                context: context);
        }
    }
}
