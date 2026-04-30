using System.Collections.Generic;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Extension methods for safely binding and unbinding <see cref="IBinder"/> instances to <see cref="IBinderAdder"/> targets.
    /// Null-safe variants guard against <see langword="null"/> binders or collections.
    /// </summary>
    public static partial class BinderExtensions
    {
        #region Singl BindSafely
        /// <summary>
        /// Binds a single binder to the provided <see cref="IBinderAdder"/> if the bindable member was found.
        /// </summary>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binder">The binder instance to bind.</param>
        /// <param name="result">The result of a bindable member lookup.</param>
        /// <param name="owner">Unused; accepted for signature parity with the collection overloads.</param>
        /// <param name="memberName">Unused; accepted for signature parity with the collection overloads.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T? binder, in FindBindableMemberResult result, object? owner = null, string? memberName = null)
            where T : IBinder
        {
            // ReSharper disable once NullableWarningSuppressionIsUsed
            if (result.IsFound)
                binder?.Bind(result.Adder!);
        }

        /// <summary>
        /// Safely binds a single binder to the specified event adder.
        /// </summary>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binder">The binder instance to bind.</param>
        /// <param name="binderAdder">The event adder to bind to.</param>
        /// <param name="owner">Unused; accepted for signature parity with the collection overloads.</param>
        /// <param name="memberName">Unused; accepted for signature parity with the collection overloads.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T? binder, IBinderAdder binderAdder, object? owner = null, string? memberName = null)
            where T : IBinder
        {
            binder?.Bind(binderAdder);
        }
        #endregion

        #region Array BindSafely
        /// <summary>
        /// Binds an array of binders to the provided <see cref="IBinderAdder"/> if the bindable member was found.
        /// </summary>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binders">The array of binders to bind.</param>
        /// <param name="result">The result of a bindable member lookup.</param>
        /// <param name="owner">Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.</param>
        /// <param name="memberName">Optional name of the field that holds <paramref name="binders"/>, used in diagnostics.</param>
        /// <exception cref="BindSafelyNullReferenceException">
        /// Thrown if any individual binder in the array is <see langword="null"/>.
        /// In Unity (<c>UNITY_2020_3_OR_NEWER</c>), skips the <see langword="null"/> binder instead of throwing.
        /// When <c>DEBUG</c> is also defined, additionally logs an error via <c>UnityEngine.Debug.LogError</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T[]? binders, in FindBindableMemberResult result, object? owner = null, string? memberName = null)
            where T : IBinder
        {
            // ReSharper disable once NullableWarningSuppressionIsUsed
            if (result.IsFound)
                binders.BindSafely(result.Adder!, owner, memberName);
        }

        /// <summary>
        /// Safely binds an array of binders to the specified event adder.
        /// </summary>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binders">The array of binders.</param>
        /// <param name="binderAdder">The event adder to bind to.</param>
        /// <param name="owner">Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.</param>
        /// <param name="memberName">Optional name of the field that holds <paramref name="binders"/>, used in diagnostics.</param>
        /// <exception cref="BindSafelyNullReferenceException">
        /// Thrown if any individual binder in the array is <see langword="null"/>.
        /// In Unity (<c>UNITY_2020_3_OR_NEWER</c>), skips the <see langword="null"/> binder instead of throwing.
        /// When <c>DEBUG</c> is also defined, additionally logs an error via <c>UnityEngine.Debug.LogError</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this T[]? binders, IBinderAdder binderAdder, object? owner = null, string? memberName = null)
            where T : IBinder
        {
            if (binders is null) return;

            for (var i = 0; i < binders.Length; i++)
            {
                var binder = binders[i];
                // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                if (binder is null)
                {
#if DEBUG
                    var message = BuildBindSafelyBinderNullMessage(i, owner, memberName);
#if UNITY_2020_3_OR_NEWER
                    UnityEngine.Debug.LogError(message, owner as UnityEngine.Object);
#else
                    throw new BindSafelyNullReferenceException(message);
#endif // UNITY_2020_3_OR_NEWER
#endif // DEBUG
                    continue;
                }
                
                binder.Bind(binderAdder);
            }
        }
        #endregion

        #region List BindSafely
        /// <summary>
        /// Binds a list of binders to the provided <see cref="IBinderAdder"/> if the bindable member was found.
        /// </summary>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binders">The list of binders.</param>
        /// <param name="result">The result of a bindable member lookup.</param>
        /// <param name="owner">Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.</param>
        /// <param name="memberName">Optional name of the field that holds <paramref name="binders"/>, used in diagnostics.</param>
        /// <exception cref="BindSafelyNullReferenceException">
        /// Thrown if any individual binder in the list is <see langword="null"/>.
        /// In Unity (<c>UNITY_2020_3_OR_NEWER</c>), skips the <see langword="null"/> binder instead of throwing.
        /// When <c>DEBUG</c> is also defined, additionally logs an error via <c>UnityEngine.Debug.LogError</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this List<T>? binders, in FindBindableMemberResult result, object? owner = null, string? memberName = null)
            where T : IBinder
        {
            // ReSharper disable once NullableWarningSuppressionIsUsed
            if (result.IsFound)
                binders.BindSafely(result.Adder!, owner, memberName);
        }

        /// <summary>
        /// Safely binds a list of binders to the specified event adder.
        /// </summary>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binders">The list of binders.</param>
        /// <param name="binderAdder">The event adder to bind to.</param>
        /// <param name="owner">Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.</param>
        /// <param name="memberName">Optional name of the field that holds <paramref name="binders"/>, used in diagnostics.</param>
        /// <exception cref="BindSafelyNullReferenceException">
        /// Thrown if any individual binder in the list is <see langword="null"/>.
        /// In Unity (<c>UNITY_2020_3_OR_NEWER</c>), skips the <see langword="null"/> binder instead of throwing.
        /// When <c>DEBUG</c> is also defined, additionally logs an error via <c>UnityEngine.Debug.LogError</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this List<T>? binders, IBinderAdder binderAdder, object? owner = null, string? memberName = null)
            where T : IBinder
        {
            if (binders is null) return;

            for (var i = 0; i < binders.Count; i++)
            {
                var binder = binders[i];
                if (binder is null)
                {
#if DEBUG
                    var message = BuildBindSafelyBinderNullMessage(i, owner, memberName);
#if UNITY_2020_3_OR_NEWER
                    UnityEngine.Debug.LogError(message, owner as UnityEngine.Object);
#else
                    throw new BindSafelyNullReferenceException(message);
#endif // UNITY_2020_3_OR_NEWER
#endif // DEBUG
                    continue;
                }
                
                binder.Bind(binderAdder);
            }
        }
        #endregion

        #region IEnumerable BindSafely
        /// <summary>
        /// Binds an enumerable of binders to the provided <see cref="IBinderAdder"/> if the bindable member was found.
        /// </summary>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binders">The enumerable of binders.</param>
        /// <param name="result">The result of a bindable member lookup.</param>
        /// <param name="owner">Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.</param>
        /// <param name="memberName">Optional name of the field that holds <paramref name="binders"/>, used in diagnostics.</param>
        /// <exception cref="BindSafelyNullReferenceException">
        /// Thrown if any individual binder in the sequence is <see langword="null"/>.
        /// In Unity (<c>UNITY_2020_3_OR_NEWER</c>), skips the <see langword="null"/> binder instead of throwing.
        /// When <c>DEBUG</c> is also defined, additionally logs an error via <c>UnityEngine.Debug.LogError</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this IEnumerable<T>? binders, in FindBindableMemberResult result, object? owner = null, string? memberName = null)
            where T : IBinder
        {
            // ReSharper disable once NullableWarningSuppressionIsUsed
            if (result.IsFound)
                binders.BindSafely(result.Adder!, owner, memberName);
        }

        /// <summary>
        /// Safely binds an enumerable of binders to the specified event adder.
        /// </summary>
        /// <typeparam name="T">The binder type that implements <see cref="IBinder"/>.</typeparam>
        /// <param name="binders">The enumerable of binders.</param>
        /// <param name="binderAdder">The event adder to bind to.</param>
        /// <param name="owner">Optional owner object (typically the View instance) used to enrich diagnostics; if it is a Unity object it is also used as the log context.</param>
        /// <param name="memberName">Optional name of the field that holds <paramref name="binders"/>, used in diagnostics.</param>
        /// <exception cref="BindSafelyNullReferenceException">
        /// Thrown if any individual binder in the sequence is <see langword="null"/>.
        /// In Unity (<c>UNITY_2020_3_OR_NEWER</c>), skips the <see langword="null"/> binder instead of throwing.
        /// When <c>DEBUG</c> is also defined, additionally logs an error via <c>UnityEngine.Debug.LogError</c>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BindSafely<T>(this IEnumerable<T>? binders, IBinderAdder binderAdder, object? owner = null, string? memberName = null)
            where T : IBinder
        {
            if (binders is null) return;

            var index = 0;
            foreach (var binder in binders)
            {
                if (binder is null)
                {
#if DEBUG
                    var message = BuildBindSafelyBinderNullMessage(index, owner, memberName);
#if UNITY_2020_3_OR_NEWER
                    UnityEngine.Debug.LogError(message, owner as UnityEngine.Object);
#else
                    throw new BindSafelyNullReferenceException(message);
#endif // UNITY_2020_3_OR_NEWER
#endif // DEBUG
                    continue;
                }
                
                binder.Bind(binderAdder);
                index++;
            }
        }
        #endregion

        private static string BuildBindSafelyBinderNullMessage(int index, object? owner, string? memberName) =>
            BuildBinderNullMessage("BindSafely", index, owner, memberName);
        
        private static string BuildBinderNullMessage(string operation, int index, object? owner, string? memberName)
        {
            var memberPart = memberName ?? "<unknown>";

            if (owner is null)
                return $"[{operation}] Binder at index {index} '{memberPart}' can't be null";

            var typeName = owner.GetType().Name;
            
#if UNITY_2020_3_OR_NEWER
            if (owner is UnityEngine.Object uo)
                return $"[{typeName}] [{operation}] Binder at index {index} '{memberPart}' of view '{typeName}' (GameObject '{uo.name}') can't be null";
#endif
            return $"[{typeName}] [{operation}] Binder at index {index} '{memberPart}' of view '{typeName}' can't be null";
        }
    }
}
