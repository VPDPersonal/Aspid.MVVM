using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A component exposing a single <see langword="float"/> field, for tests that bind through a
    /// Component binder base.
    /// </summary>
    internal sealed class FloatComponent : MonoBehaviour
    {
        public float Value;
    }
}
