using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A component exposing a single <see cref="Texture2D"/> field, for tests that bind through a
    /// Component object binder base.
    /// </summary>
    internal sealed class ObjectComponent : MonoBehaviour
    {
        public Texture2D Value;
    }
}
