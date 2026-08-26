// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Reports a failure and hands back the fallback in one call.
    /// </summary>
    public static class ConverterFallbackExtensions
    {
        /// <summary>
        /// Reports the failure and returns the specified fallback.
        /// </summary>
        /// <typeparam name="T">The type the converter returns.</typeparam>
        /// <param name="converter">The failing converter — pass <see langword="this"/>.</param>
        /// <param name="fallback">Returned instead of the value that would not convert.</param>
        /// <param name="problem">What is wrong, as a sentence without the trailing period.</param>
        /// <returns><paramref name="fallback"/>.</returns>
        public static T UseFallback<T>(this IConverter converter, T fallback, string problem)
        {
            converter.LogError(
                problem: problem,
                consequence: "Using the fallback.");
            
            return fallback;
        }
    }
}
