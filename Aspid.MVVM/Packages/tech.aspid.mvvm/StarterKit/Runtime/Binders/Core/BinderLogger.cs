using System;
using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Writes binder messages in one shape shared by all binders.
    /// </summary>
    /// <remarks>
    /// The <see cref="Type"/> overloads are for helpers reporting on another binder's behalf.
    /// </remarks>
    public static class BinderLogger
    {
        private const string Prefix = "[Aspid.MVVM] ";

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="binder">The logging binder; pinged when it is a scene or asset object.</param>
        /// <param name="message">The message, as full sentences.</param>
        /// <param name="context">The object to ping instead of the binder.</param>
        [HideInCallstack]
        public static void Log(this IBinder binder, string message, Object? context = null) =>
            Log(binder.GetType(), message, context ?? binder as Object);

        /// <summary>
        /// Logs an informational message on behalf of <paramref name="binderType"/>.
        /// </summary>
        /// <param name="binderType">The logging binder's type.</param>
        /// <param name="message">The message, as full sentences.</param>
        /// <param name="context">The object to ping, when one is known.</param>
        [HideInCallstack]
        public static void Log(Type binderType, string message, Object? context = null)
        {
            var binderName = binderType.GetTypeName();
            Debug.Log($"{Prefix}{binderName}: {message}", context);
        }

        /// <summary>
        /// Reports a setup the binder still works with, but not the way it reads.
        /// </summary>
        /// <param name="binder">The reporting binder; pinged when it is a scene or asset object.</param>
        /// <param name="problem">What is wrong, as a sentence without the trailing period.</param>
        /// <param name="consequence">What the binder does instead, as a full sentence.</param>
        /// <param name="context">The object to ping instead of the binder.</param>
        [HideInCallstack]
        public static void LogWarning(this IBinder binder, string problem, string consequence, Object? context = null) =>
            LogWarning(binder.GetType(), problem, consequence, context ?? binder as Object);

        /// <summary>
        /// Reports a questionable setup on behalf of <paramref name="binderType"/>.
        /// </summary>
        /// <param name="binderType">The reporting binder's type.</param>
        /// <param name="problem">What is wrong, as a sentence without the trailing period.</param>
        /// <param name="consequence">What the binder does instead, as a full sentence.</param>
        /// <param name="context">The object to ping, when one is known.</param>
        [HideInCallstack]
        public static void LogWarning(Type binderType, string problem, string consequence, Object? context = null)
        {
            var binderName = binderType.GetTypeName();
            Debug.LogWarning($"{Prefix}{binderName}: {problem}. {consequence}", context);
        }

        /// <summary>
        /// Reports a problem: a value the target will not take, a missing reference, a bad setting.
        /// </summary>
        /// <param name="binder">The reporting binder; pinged when it is a scene or asset object.</param>
        /// <param name="problem">What is wrong, as a sentence without the trailing period.</param>
        /// <param name="consequence">What the binder does instead, as a full sentence.</param>
        /// <param name="context">The object to ping instead of the binder.</param>
        [HideInCallstack]
        public static void LogError(this IBinder binder, string problem, string consequence, Object? context = null) =>
            LogError(binder.GetType(), problem, consequence, context ?? binder as Object);

        /// <summary>
        /// Reports a problem on behalf of <paramref name="binderType"/>.
        /// </summary>
        /// <param name="binderType">The reporting binder's type.</param>
        /// <param name="problem">What is wrong, as a sentence without the trailing period.</param>
        /// <param name="consequence">What the binder does instead, as a full sentence.</param>
        /// <param name="context">The object to ping, when one is known.</param>
        [HideInCallstack]
        public static void LogError(Type binderType, string problem, string consequence, Object? context = null)
        {
            var binderName = binderType.GetTypeName();
            Debug.LogError($"{Prefix}{binderName}: {problem}. {consequence}", context);
        }

        /// <summary>
        /// Reports an exception the binder caught.
        /// </summary>
        /// <param name="binder">The throwing binder; pinged when it is a scene or asset object.</param>
        /// <param name="exception">The exception caught.</param>
        /// <param name="consequence">What the binder does instead, as a full sentence.</param>
        /// <param name="context">The object to ping instead of the binder.</param>
        [HideInCallstack]
        public static void LogError(this IBinder binder, Exception exception, string consequence, Object? context = null) =>
            LogError(binder.GetType(), exception, consequence, context ?? binder as Object);

        /// <summary>
        /// Reports an exception on behalf of <paramref name="binderType"/>.
        /// </summary>
        /// <param name="binderType">The throwing binder's type.</param>
        /// <param name="exception">The exception caught.</param>
        /// <param name="consequence">What the binder does instead, as a full sentence.</param>
        /// <param name="context">The object to ping, when one is known.</param>
        [HideInCallstack]
        public static void LogError(Type binderType, Exception exception, string consequence, Object? context = null)
        {
            var binderName = binderType.GetTypeName();
            var exceptionName = exception.GetType().GetTypeName();

            Debug.LogError(
                message: $"{Prefix}{binderName}: threw {exceptionName} ({exception.Message}). {consequence}\n{exception}",
                context: context);
        }
    }
}
