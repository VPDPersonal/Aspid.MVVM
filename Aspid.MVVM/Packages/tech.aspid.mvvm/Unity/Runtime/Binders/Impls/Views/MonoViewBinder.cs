using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="ViewTargetBinder{TView}"/> restricted to <see cref="Component"/>-based views.
    /// </summary>
    /// <typeparam name="TView">The type of <see cref="Component"/> that implements <see cref="IView"/>.</typeparam>
    public class MonoViewBinder<TView> : ViewTargetBinder<TView>
        where TView : Component, IView
    {
        /// <inheritdoc/>
        public MonoViewBinder(TView target, BindMode mode = BindMode.OneWay)
            : base(target, mode) { }
    }

    /// <summary>
    /// <see cref="MonoViewBinder{TView}"/> for <see cref="MonoView"/>.
    /// </summary>
    public class MonoViewBinder : MonoViewBinder<MonoView>
    {
        /// <inheritdoc/>
        public MonoViewBinder(MonoView target, BindMode mode = BindMode.OneWay)
            : base(target, mode) { }
    }
}
