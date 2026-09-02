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
        /// Returns the parameter type a binder over <typeparamref name="T"/> sets,
        /// or <see langword="null"/> when <typeparamref name="T"/> is none an Animator parameter can hold.
        /// </summary>
        /// <remarks>
        /// <see cref="AnimatorControllerParameterType.Trigger"/> is never inferred: a trigger carries no value,
        /// so the binders that fire one name it explicitly instead.
        /// </remarks>
        public static AnimatorControllerParameterType? Of<T>()
        {
            if (typeof(T) == typeof(float)) return AnimatorControllerParameterType.Float;
            if (typeof(T) == typeof(int)) return AnimatorControllerParameterType.Int;
            if (typeof(T) == typeof(bool)) return AnimatorControllerParameterType.Bool;

            return null;
        }
    }
}
