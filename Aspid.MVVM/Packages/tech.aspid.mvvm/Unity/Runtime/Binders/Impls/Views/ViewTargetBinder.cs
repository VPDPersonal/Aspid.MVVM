using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract base <see cref="TargetBinder{TTarget}"/> that initializes the target view with the bound <see cref="IViewModel"/>
    /// and deinitializes it on unbind.
    /// </summary>
    /// <typeparam name="TView">The type of <see cref="Object"/> that implements <see cref="IView"/>.</typeparam>
    public abstract class ViewTargetBinder<TView> : TargetBinder<TView>, IBinder<IViewModel>
        where TView : Object, IView
    {
        /// <param name="target">The view to bind.</param>
        /// <param name="mode">The binding mode. Must be <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="target"/> is <see langword="null"/>.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown when <paramref name="mode"/> is not <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.</exception>
        public ViewTargetBinder(TView target, BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.ThrowExceptionIfNotOne();
        }

        /// <summary>
        /// Deinitializes the view, then initializes it with <paramref name="viewModel"/> unless it is <see langword="null"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel received from the binding, or <see langword="null"/> to deinitialize only.</param>
        public void SetValue(IViewModel viewModel)
        {
            DeinitializeView();

            if (viewModel is not null)
                InitializeView(viewModel);
        }

        /// <summary>
        /// Deinitializes the view.
        /// </summary>
        protected override void OnUnbound() =>
            DeinitializeView();

        /// <summary>
        /// Initializes the target view with <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel to initialize the view with.</param>
        protected void InitializeView(IViewModel viewModel) =>
            Target.Initialize(viewModel);

        /// <summary>
        /// Deinitializes the target view.
        /// </summary>
        protected void DeinitializeView() =>
            Target.Deinitialize();
    }
}
