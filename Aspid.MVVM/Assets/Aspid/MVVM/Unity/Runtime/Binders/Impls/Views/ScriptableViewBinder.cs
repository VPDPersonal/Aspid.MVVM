using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="ViewTargetBinder{TView}"/> restricted to <see cref="ScriptableObject"/>-based views.
    /// </summary>
    /// <typeparam name="TView">The type of <see cref="ScriptableObject"/> that implements <see cref="IView"/>.</typeparam>
    public class ScriptableViewBinder<TView> : ViewTargetBinder<TView>
        where TView : ScriptableObject, IView
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ScriptableViewBinder{TView}"/> for the specified view target.
        /// </summary>
        /// <param name="target">The ScriptableObject view to bind.</param>
        /// <param name="mode">The binding mode. Only one-directional modes are supported.</param>
        public ScriptableViewBinder(TView target, BindMode mode = BindMode.OneWay)
            : base(target, mode) { }
    }

    /// <summary>
    /// <see cref="ScriptableViewBinder{TView}"/> specialized for <see cref="ScriptableView"/> assets.
    /// </summary>
    public class ScriptableViewBinder : ScriptableViewBinder<ScriptableView>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ScriptableViewBinder"/> for the specified <see cref="ScriptableView"/>.
        /// </summary>
        /// <param name="target">The <see cref="ScriptableView"/> to bind.</param>
        /// <param name="mode">The binding mode. Only one-directional modes are supported.</param>
        public ScriptableViewBinder(ScriptableView target, BindMode mode = BindMode.OneWay)
            : base(target, mode) { }
    }
}