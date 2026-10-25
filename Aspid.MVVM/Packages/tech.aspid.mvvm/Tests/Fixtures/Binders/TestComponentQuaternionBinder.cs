using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A <see cref="ComponentMonoBinder{TComponent, TProperty}"/> over <see cref="QuaternionComponent"/>, for tests of the rotation channels.
    /// </summary>
    public sealed class TestComponentQuaternionBinder : ComponentMonoBinder<QuaternionComponent, Quaternion>, IRotationBinder
    {
        protected override Quaternion Property
        {
            get => CachedComponent.Value;
            set => CachedComponent.Value = value;
        }
    }
}
