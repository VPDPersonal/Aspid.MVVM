using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A <see cref="ComponentIntMonoBinder{TComponent}"/> over <see cref="IntComponent"/>, for tests of the reverse channel.
    /// </summary>
    public sealed class TestComponentIntBinder : ComponentIntMonoBinder<IntComponent>
    {
        protected override int Property
        {
            get => CachedComponent.Value;
            set => CachedComponent.Value = value;
        }
    }
}
