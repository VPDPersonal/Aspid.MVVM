using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// A composite binder interface that accepts both <see cref="Vector2"/> and <see cref="Vector3"/> values.
    /// </summary>
    public interface IVectorBinder : IBinder<Vector2>, IBinder<Vector3> { }
}