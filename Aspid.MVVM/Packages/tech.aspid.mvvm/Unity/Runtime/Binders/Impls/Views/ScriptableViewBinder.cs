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
        /// <inheritdoc/>
        public ScriptableViewBinder(TView target, BindMode mode = BindMode.OneWay)
            : base(target, mode) { }
    }

    /// <summary>
    /// <see cref="ScriptableViewBinder{TView}"/> for <see cref="ScriptableView"/>.
    /// </summary>
    public class ScriptableViewBinder : ScriptableViewBinder<ScriptableView>
    {
        /// <inheritdoc/>
        public ScriptableViewBinder(ScriptableView target, BindMode mode = BindMode.OneWay)
            : base(target, mode) { }
    }
}
