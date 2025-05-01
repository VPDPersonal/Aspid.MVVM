namespace Aspid.MVVM.StarterKit
{
    public interface IBindableProperty<T> : IReadOnlyBindableProperty<T>
    {
        public new T? Value { get; set; }
    }
}