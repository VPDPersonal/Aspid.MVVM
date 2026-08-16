#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// The shared half of <see cref="ConverterFailureMode"/>: reporting a value that would not convert.
    /// </summary>
    /// <remarks>
    /// One message shape and one log-once rule, so a designer reading the console sees the same thing
    /// whichever converter failed.
    /// <para>
    /// The log-once flag lives on the calling converter, passed by reference, because "once" has to mean
    /// once per converter instance: a binder pushes on every notification and a bad value fails on each,
    /// and <see cref="UnityEngine.Debug.LogError"/> captures a stack trace — logging per call costs frames.
    /// </para>
    /// </remarks>
    internal static class ConverterFailure
    {
        /// <summary>
        /// Reports a value the converter could not convert, once per converter instance.
        /// </summary>
        /// <param name="logged">
        /// The caller's log-once flag. Set to <see langword="true"/> by the first call.
        /// </param>
        /// <param name="converter">The reporting converter's type name.</param>
        /// <param name="value">The value that would not convert.</param>
        /// <param name="expected">What the converter needed, as a noun phrase — "a whole number".</param>
        /// <param name="fallback">What is being returned instead, as it will read in the sentence.</param>
        internal static void Report(
            ref bool logged,
            string converter,
            object? value,
            string expected,
            string fallback)
        {
            if (logged) return;
            logged = true;

            UnityEngine.Debug.LogError(
                $"{converter}: expected {expected} but got \"{value}\". Using {fallback}. "
                + "Further failures on this converter are not reported.");
        }

        /// <summary>
        /// Builds the exception thrown when the converter is set to
        /// <see cref="ConverterFailureMode.Throw"/>.
        /// </summary>
        /// <param name="converter">The throwing converter's type name.</param>
        /// <param name="value">The value that would not convert.</param>
        /// <param name="expected">What the converter needed, as a noun phrase.</param>
        /// <returns>The exception to throw.</returns>
        internal static FormatException Rejected(string converter, object? value, string expected) =>
            new($"{converter}: expected {expected} but got \"{value}\".");
    }
}
