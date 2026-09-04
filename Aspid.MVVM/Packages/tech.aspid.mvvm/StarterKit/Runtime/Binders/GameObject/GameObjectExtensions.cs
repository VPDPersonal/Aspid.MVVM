using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// Extension methods for <see cref="GameObject"/> used by the game object binders.
    /// </summary>
    public static class GameObjectExtensions
    {
        private const int MaxLayer = 31;

        /// <summary>
        /// Sets <see cref="GameObject.layer"/> when <paramref name="layer"/> is a valid index; otherwise reports it.
        /// </summary>
        /// <param name="gameObject">The object whose layer is set.</param>
        /// <param name="layer">The layer index, 0 to 31.</param>
        /// <param name="binder">The binder writing the layer; named in the diagnostic.</param>
        public static void SetLayer(this GameObject gameObject, int layer, IBinder binder)
        {
            if (layer is >= 0 and <= MaxLayer)
            {
                gameObject.layer = layer;
                return;
            }

            binder.LogError(
                problem: $"layer {layer} does not exist",
                consequence: "The layer is left unchanged.",
                context: gameObject);
        }
    }
}
