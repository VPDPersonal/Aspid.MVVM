#nullable enable
namespace Aspid.MVVM.StarterKit.Binders
{
    public interface IBindableProperty<T> : IReadOnlyBindableProperty<T>
    {
        public new T? Value { get; set; }
    }
}