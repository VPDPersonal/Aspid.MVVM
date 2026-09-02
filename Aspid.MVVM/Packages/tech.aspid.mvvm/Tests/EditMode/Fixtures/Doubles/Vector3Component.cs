using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A component exposing a single <see cref="Vector3"/> field, for tests that bind through a
    /// Component binder base.
    /// </summary>
    internal sealed class Vector3Component : MonoBehaviour
    {
        public Vector3 Value;
    }
}
