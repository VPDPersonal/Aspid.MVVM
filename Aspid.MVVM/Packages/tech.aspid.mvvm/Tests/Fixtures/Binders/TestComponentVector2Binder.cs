using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A <see cref="ComponentMonoBinder{TComponent, TProperty}"/> over <see cref="Vector2Component"/>, for tests of the Component binder base.
    /// </summary>
    public sealed class TestComponentVector2Binder : ComponentMonoBinder<Vector2Component, Vector2>, IVector2Binder
    {
        protected override Vector2 Property
        {
            get => CachedComponent.Value;
            set => CachedComponent.Value = value;
        }
    }
}
