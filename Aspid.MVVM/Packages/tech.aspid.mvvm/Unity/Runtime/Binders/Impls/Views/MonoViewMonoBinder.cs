using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent}"/> that adds <see cref="IViewModel"/> binding support,
    /// initializing the cached <typeparamref name="TView"/> component view when a bound <see cref="IViewModel"/> is received.
    /// </summary>
    /// <typeparam name="TView">The type of <see cref="Component"/> that implements <see cref="IView"/>.</typeparam>
    public abstract class MonoViewMonoBinder<TView> : ComponentMonoBinder<TView>, IBinder<IViewModel>
        where TView : Component, IView
    {
        /// <summary>
        /// Called when the bound <see cref="IViewModel"/> value is received.
        /// Deinitializes the current view and initializes it with the new ViewModel if it is not <see langword="null"/>.
        /// </summary>
        /// <param name="viewModel">The new ViewModel value, or <see langword="null"/> to deinitialize.</param>
        [BinderLog]
        public void SetValue(IViewModel viewModel)
        {
            DeinitializeView();

            if (viewModel is not null)
                InitializeView(viewModel);
        }

        /// <summary>
        /// Called after unbinding is complete.
        /// Deinitializes the view, detaching any bound <see cref="IViewModel"/>.
        /// </summary>
        protected override void OnUnbound() =>
            DeinitializeView();

        /// <summary>
        /// Initializes the cached view component with the specified ViewModel.
        /// </summary>
        /// <param name="viewModel">The ViewModel to bind to the view.</param>
        protected void InitializeView(IViewModel viewModel) =>
            CachedComponent.Initialize(viewModel);

        /// <summary>
        /// Deinitializes the cached view component, detaching any bound ViewModel.
        /// </summary>
        protected void DeinitializeView() =>
            CachedComponent.Deinitialize();
    }

    /// <summary>
    /// <see cref="MonoViewMonoBinder{TView}"/> specialized for <see cref="MonoView"/> components.
    /// </summary>
    [AddBinderContextMenu(typeof(MonoView))]
    [AddComponentMenu("Aspid/MVVM/Binders/Views/MonoView Binder")]
    public class MonoViewMonoBinder : MonoViewMonoBinder<MonoView> { }
}