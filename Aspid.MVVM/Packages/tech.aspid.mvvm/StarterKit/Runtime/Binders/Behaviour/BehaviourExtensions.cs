using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extension methods used by the behaviour binders.
    /// </summary>
    internal static class BehaviourExtensions
    {
        /// <summary>
        /// Returns the first <see cref="Behaviour"/> on the object that is not a binder, so a binder never resolves
        /// itself.
        /// </summary>
        /// <param name="gameObject">The object to search.</param>
        /// <returns>The behaviour, or <see langword="null"/> when there is none.</returns>
        public static Behaviour GetFirstNonBinderBehaviour(this GameObject gameObject)
        {
            foreach (var behaviour in gameObject.GetComponents<Behaviour>())
            {
                if (behaviour is not MonoBinder) return behaviour;
            }

            return null;
        }
    }
}
