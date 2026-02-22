using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract <see cref="MonoBinder"/> that binds a <typeparamref name="TView"/> component
    /// to an <see cref="IViewModel"/> property.
    /// Resolves the view component via <see cref="ComponentMonoBinder{TComponent}.CachedComponent"/>.
    /// </summary>
    /// <typeparam name="TView">The type of the view component. Must inherit from <see cref="Component"/> and implement <see cref="IView"/>.</typeparam>
    public abstract class MonoViewMonoBinder<TView> : ComponentMonoBinder<TView>, IBinder<IViewModel>
        where TView : Component, IView
    {
        /// <summary>
        /// Called when the bound ViewModel value changes.
        /// Deinitializes the current view and initializes it with the new ViewModel if it is not <c>null</c>.
        /// </summary>
        /// <param name="viewModel">The new ViewModel value, or <c>null</c> to deinitialize.</param>
        [BinderLog]
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
    /// Concrete <see cref="MonoBinder"/> for <see cref="MonoView"/> components.
    /// </summary>
    [AddBinderContextMenu(typeof(MonoView))]
    [AddComponentMenu("Aspid/MVVM/Binders/Views/MonoView Binder")]
    public class MonoViewMonoBinder : MonoViewMonoBinder<MonoView> { }
}