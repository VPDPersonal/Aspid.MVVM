#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// What a converter does with a value it cannot convert, and what it returns instead.
    /// </summary>
    /// <typeparam name="T">The type the converter returns.</typeparam>
    [Serializable]
    public struct ConverterFallback<T>
    {
        /// <summary>
        /// Gets what the converter does with a value it cannot convert.
        /// </summary>
        [field: Tooltip("What to do with a value that will not convert.")]
        [field: SerializeField]
        public ConverterFailureMode Mode { get; private set; }

        /// <summary>
        /// Gets the value returned instead of one that will not convert.
        /// </summary>
        [field: Tooltip("Returned instead of a value that will not convert.")]
        [field: SerializeField]
        public T FallbackValue { get; private set; }

        /// <param name="value">Returned instead of a value that will not convert.</param>
        /// <param name="mode">
        /// What to do with a value that will not convert. <see cref="ConverterFailureMode.ReturnInput"/>
        /// passes it through when it fits the output type, and otherwise uses the fallback.
        /// </param>
        public ConverterFallback(
            T value,
            ConverterFailureMode mode = ConverterFailureMode.ReturnFallback)
        {
            Mode = mode;
            FallbackValue = value;
        }

        /// <summary>
        /// Reports the failure and returns what <see cref="Mode"/> says to.
        /// </summary>
        /// <param name="converter">The failing converter — pass <see langword="this"/>.</param>
        /// <param name="value">The value that would not convert.</param>
        /// <param name="problem">What is wrong, as a sentence without the trailing period.</param>
        /// <returns>
        /// The value itself when <see cref="Mode"/> is <see cref="ConverterFailureMode.ReturnInput"/>
        /// and the value already is a <typeparamref name="T"/>; otherwise, <see cref="FallbackValue"/>.
        /// </returns>
        public readonly T Fail(IConverter converter, object? value, string problem)
        {
            if (Mode is ConverterFailureMode.ReturnInput)
            {
                if (value is T input)
                {
                    converter.LogError(
                        problem: problem,
                        consequence: "Returning the input unchanged.");

                    return input;
                }

                converter.LogError(
                    problem: problem,
                    consequence: $"Return Input is set, but the input is not a {typeof(T).GetTypeName()}; using the fallback.");

                return FallbackValue;
            }

            converter.LogError(problem, "Using the fallback.");
            return FallbackValue;
        }

        /// <summary>
        /// Wraps the specified value as a fallback with <see cref="ConverterFailureMode.ReturnFallback"/>.
        /// </summary>
        /// <param name="value">Returned instead of a value that will not convert.</param>
        /// <returns>The fallback.</returns>
        public static implicit operator ConverterFallback<T>(T value) => new(value);
    }
}
