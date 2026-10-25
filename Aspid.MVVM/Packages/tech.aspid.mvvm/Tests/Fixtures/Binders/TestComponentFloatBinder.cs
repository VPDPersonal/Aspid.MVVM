using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A <see cref="ComponentFloatMonoBinder{TComponent}"/> over <see cref="FloatComponent"/>, for tests of the reverse channel.
    /// </summary>
    public sealed class TestComponentFloatBinder : ComponentFloatMonoBinder<FloatComponent>
    {
        protected override float Property
        {
            get => CachedComponent.Value;
            set => CachedComponent.Value = value;
        }
    }
}
