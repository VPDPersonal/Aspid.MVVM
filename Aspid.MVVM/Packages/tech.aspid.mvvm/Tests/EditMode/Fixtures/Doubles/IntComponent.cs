using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A component exposing a single <see langword="int"/> field, for tests that bind through a
    /// Component binder base.
    /// </summary>
    internal sealed class IntComponent : MonoBehaviour
    {
        public int Value;
    }
}
