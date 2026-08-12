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
        /// A plain <c>GetComponent&lt;Behaviour&gt;</c> matches every behaviour on the object, binders included, and
        /// component order decides which one wins. On an object carrying little else that is the binder itself — so
        /// the binder would enable and disable itself, stop receiving values, and leave nothing in the log to
        /// explain it. Skipping binders removes that outcome; which of the remaining behaviours is meant is still
        /// the author's choice, made by filling the field.
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
