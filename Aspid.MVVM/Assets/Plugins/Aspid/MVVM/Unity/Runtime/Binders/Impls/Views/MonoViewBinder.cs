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
        /// <summary>
        /// Initializes a new instance of <see cref="MonoViewBinder{TView}"/> for the specified view target.
        /// </summary>
        /// <param name="target">The view component to bind.</param>
        /// <param name="mode">The binding mode. Only one-directional modes are supported.</param>
        public MonoViewBinder(TView target, BindMode mode = BindMode.OneWay)
            : base(target, mode) { }
    }

    /// <summary>
    /// <see cref="MonoViewBinder{TView}"/> specialized for <see cref="MonoView"/> components.
    /// </summary>
    public class MonoViewBinder : MonoViewBinder<MonoView>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MonoViewBinder"/> for the specified <see cref="MonoView"/>.
        /// </summary>
        /// <param name="target">The <see cref="MonoView"/> to bind.</param>
        /// <param name="mode">The binding mode. Only one-directional modes are supported.</param>
        public MonoViewBinder(MonoView target, BindMode mode = BindMode.OneWay)
            : base(target, mode) { }
    }
}