using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that initializes a <typeparamref name="TView"/> when a bound <see cref="IViewModel"/> is received,
    /// and deinitializes it on unbind.
    /// </summary>
    /// <typeparam name="TView">The type of <see cref="Object"/> that implements <see cref="IView"/>.</typeparam>
    public abstract class ViewTargetBinder<TView> : TargetBinder<TView>, IBinder<IViewModel>
        where TView : Object, IView
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ViewTargetBinder{TView}"/> for the specified view target.
        /// </summary>
        /// <param name="target">The view component to bind.</param>
        /// <param name="mode">The binding mode. Only one-directional modes are supported.</param>
        public ViewTargetBinder(TView target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }

        /// <summary>
        /// Called when the bound <see cref="IViewModel"/> value is received.
        /// Deinitializes the view first, then initializes it with <paramref name="viewModel"/> if it is not <see langword="null"/>.
        /// </summary>
        /// <param name="viewModel">The new ViewModel, or <see langword="null"/> to deinitialize without reinitializing.</param>
        public void SetValue(IViewModel viewModel)
        {
            DeinitializeView();

            if (viewModel is not null)
                InitializeView(viewModel);
        }

        /// <summary>
        /// Called after unbinding. Deinitializes the target view.
        /// </summary>
        protected override void OnUnbound() =>
            DeinitializeView();

        /// <summary>
        /// Initializes the target <typeparamref name="TView"/> with <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel to initialize the view with.</param>
        protected void InitializeView(IViewModel viewModel) =>
            Target.Initialize(viewModel);

        /// <summary>
        /// Deinitializes the target <typeparamref name="TView"/>, clearing its current ViewModel.
        /// </summary>
        protected void DeinitializeView() =>
            Target.Deinitialize();
    }
}