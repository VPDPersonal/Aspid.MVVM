using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A <see cref="ComponentMonoBinder{TComponent, TProperty}"/> over <see cref="Vector3Component"/> that exposes the applied value.
    /// </summary>
    public sealed class TestComponentVector3Binder : ComponentMonoBinder<Vector3Component, Vector3>, IVector3Binder
    {
        public Vector3 Applied => CachedComponent.Value;

        protected override Vector3 Property
        {
            get => CachedComponent.Value;
            set => CachedComponent.Value = value;
        }
    }
}
