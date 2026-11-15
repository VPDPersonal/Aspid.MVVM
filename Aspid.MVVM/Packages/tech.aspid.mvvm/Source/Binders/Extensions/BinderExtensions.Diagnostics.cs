using System;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    public static partial class BinderExtensions
    {
        private static bool IsMissing<T>(T binder) =>
            binder is null || binder is Object unityObject && !unityObject;

        [Conditional(conditionString: "DEBUG")]
        private static void ReportMissingBinder(string operation, int? index, object? owner, string? memberName)
        {
            if (!index.HasValue) return;

            Debug.LogError(
                message: $"{BuildBinderContext(operation, index, owner, memberName)} is null or destroyed.",
                context: GetLogContext(owner));
        }

        private static void ReportBinderFailure(
            string operation,
            int? index,
            object? owner,
            string? memberName,
            Exception exception)
        {
            var message = $"{BuildBinderContext(operation, index, owner, memberName)} " + 
                $"threw {exception.GetType().Name}: {exception.Message}";

            // Keep context in the Console title and use the exception's stack trace.
            Debug.LogFormat(
                logType: LogType.Exception,
                logOptions: LogOption.NoStacktrace,
                context: GetLogContext(owner),
                format: "{0}\n{1}",
                args: new object[] { message, exception });
        }

        private static Object? GetLogContext(object? owner) => owner is Object context && context
            ? context
            : null;

        private static string BuildBinderContext(string operation, int? index, object? owner, string? memberName)
        {
            var binderPart = index.HasValue
                ? $"Binder at index {index.Value}"
                : "Binder";
            
            var message = $"[{operation}] {binderPart} '{memberName ?? "<unknown>"}'";
            if (owner is null) return message;

            message = $"[{owner.GetType().Name}] {message}";
            if (owner is not Object unityOwner) return message;

            return !unityOwner 
                ? $"{message} (destroyed Unity object)" 
                : $"{message} (Object '{unityOwner.name}')";
        }
    }
}
