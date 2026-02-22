using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Non-MonoBehaviour binder that binds a <typeparamref name="TView"/> ScriptableObject view to an <see cref="IViewModel"/> property.
    /// Initializes the view when a ViewModel is set and deinitializes it when unbound or when <c>null</c> is assigned.
    /// </summary>
    /// <typeparam name="TView">The type of the ScriptableObject view. Must inherit from <see cref="ScriptableObject"/> and implement <see cref="IView"/>.</typeparam>
    public class ScriptableViewBinder<TView> : TargetBinder<TView>
        where TView : ScriptableObject, IView
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ScriptableViewBinder{TView}"/> for the specified view target.
        /// </summary>
        /// <param name="target">The ScriptableObject view to bind.</param>
        /// <param name="mode">The binding mode. Only one-directional modes are supported.</param>
        public ScriptableViewBinder(TView target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }

        /// <summary>
        /// Called when the bound ViewModel value changes.
        /// Deinitializes the current view and initializes it with the new ViewModel if it is not <c>null</c>.
        /// </summary>
        /// <param name="viewModel">The new ViewModel value, or <c>null</c> to deinitialize.</param>
        public void SetValue(IViewModel viewModel)
        {
            DeinitializeView();

            if (viewModel is not null)
                InitializeView(viewModel);
        }

        /// <inheritdoc/>
        protected override void OnUnbound() =>
            DeinitializeView();

        /// <summary>
        /// Initializes the target view with the specified ViewModel.
        /// </summary>
        /// <param name="viewModel">The ViewModel to bind to the view.</param>
        protected void InitializeView(IViewModel viewModel) =>
            Target.Initialize(viewModel);

        /// <summary>
        /// Deinitializes the target view, detaching any bound ViewModel.
        /// </summary>
        protected void DeinitializeView() =>
            Target.Deinitialize();
    }

    /// <summary>
    /// Concrete binder for <see cref="ScriptableView"/> assets.
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