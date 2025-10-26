// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public interface IBindableValue<T> : IReadOnlyBindableValue<T>
    {
        public new T? Value { get; set; }
    }
}