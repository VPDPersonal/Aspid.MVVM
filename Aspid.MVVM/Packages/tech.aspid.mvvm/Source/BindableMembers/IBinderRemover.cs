// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Defines the side of a bindable member that detaches binders.
    /// </summary>
    public interface IBinderRemover
    {
        /// <summary>
        /// Unsubscribes <paramref name="binder"/> from the member.
        /// </summary>
        /// <param name="binder">The binder previously passed to <see cref="IBinderAdder.Add(IBinder)"/>.</param>
        public void Remove(IBinder binder);
    }
}
