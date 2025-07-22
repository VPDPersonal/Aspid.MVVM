namespace Aspid.Collections.Observable;

public interface IObservableEvents<out T> : IDisposable
{
    public event Action Reset;
    public event Action<IReadOnlyList<T?>, int> Added;
    public event Action<IReadOnlyList<T?>, int> Removed;
    public event Action<IReadOnlyList<T?>, int, int> Moved;
    public event Action<IReadOnlyList<T?>, IReadOnlyList<T?>, int> Replaced;
}