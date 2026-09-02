using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A component exposing a single <see cref="Quaternion"/> field, for tests that bind through a
    /// Component binder base.
    /// </summary>
    internal sealed class QuaternionComponent : MonoBehaviour
    {
        public Quaternion Value = Quaternion.identity;
    }
}
