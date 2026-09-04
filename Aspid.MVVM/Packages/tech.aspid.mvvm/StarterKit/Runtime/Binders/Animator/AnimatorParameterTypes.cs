using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Maps a bound value type to the <see cref="AnimatorControllerParameterType"/> it addresses.
    /// </summary>
    internal static class AnimatorParameterTypes
    {
        /// <summary>
        /// Returns the parameter type for <typeparamref name="T"/>, or <see langword="null"/> when no parameter holds
        /// it. <see cref="AnimatorControllerParameterType.Trigger"/> is never inferred.
        /// </summary>
        /// <typeparam name="T">The bound value type.</typeparam>
        /// <returns>The parameter type, or <see langword="null"/>.</returns>
        public static AnimatorControllerParameterType? Of<T>()
        {
            if (typeof(T) == typeof(float)) return AnimatorControllerParameterType.Float;
            if (typeof(T) == typeof(int)) return AnimatorControllerParameterType.Int;
            if (typeof(T) == typeof(bool)) return AnimatorControllerParameterType.Bool;

            return null;
        }
    }
}
