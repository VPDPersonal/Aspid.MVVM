using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Picks the <see cref="Behaviour"/> a behaviour binder should drive when its own field is left empty.
    /// </summary>
    internal static class BehaviourResolution
    {
        /// <summary>
        /// Returns the first <see cref="Behaviour"/> on <paramref name="gameObject"/> that is not itself a binder.
        /// </summary>
        /// <remarks>
        /// Skips components that are themselves binders, so a binder cannot end up resolving — and disabling — itself.
        /// </remarks>
        /// <param name="gameObject">The object to search.</param>
        /// <returns>The first non-binder <see cref="Behaviour"/>, or <see langword="null"/> when there is none.</returns>
        public static Behaviour FirstThatIsNotABinder(GameObject gameObject)
        {
            var behaviours = gameObject.GetComponents<Behaviour>();

            foreach (var behaviour in behaviours)
            {
                if (behaviour is not MonoBinder) return behaviour;
            }

            return null;
        }
    }
}
