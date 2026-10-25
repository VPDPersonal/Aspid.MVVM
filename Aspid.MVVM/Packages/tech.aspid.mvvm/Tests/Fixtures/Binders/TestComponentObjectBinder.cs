using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A <see cref="ComponentObjectMonoBinder{TComponent, TProperty}"/> over <see cref="ObjectComponent"/>, for tests of the destroyed-object handling.
    /// </summary>
    public sealed class TestComponentObjectBinder : ComponentObjectMonoBinder<ObjectComponent, Texture2D>
    {
        protected override Texture2D Property
        {
            get => CachedComponent.Value;
            set => CachedComponent.Value = value;
        }
    }
}
