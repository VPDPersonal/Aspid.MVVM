using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract base <see cref="ComponentMonoBinder{TComponent}"/> that initializes the target view with the bound <see cref="IViewModel"/>.
    /// </summary>
    /// <typeparam name="TView">The type of <see cref="Component"/> that implements <see cref="IView"/>.</typeparam>
    public abstract partial class MonoViewMonoBinder<TView> : ComponentMonoBinder<TView>, IBinder<IViewModel>
        where TView : Component, IView
    {
        /// <summary>
        /// Deinitializes the view, then initializes it with <paramref name="viewModel"/> unless it is <see langword="null"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel received from the binding, or <see langword="null"/> to deinitialize only.</param>
        [BinderLog]
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
            CachedComponent.Initialize(viewModel);

        /// <summary>
        /// Deinitializes the target view.
        /// </summary>
        protected void DeinitializeView() =>
            CachedComponent.Deinitialize();
    }

    /// <summary>
    /// <see cref="MonoViewMonoBinder{TView}"/> for <see cref="MonoView"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(MonoView))]
    [AddComponentMenu("Aspid/MVVM/Binders/Views/MonoView Binder")]
    public class MonoViewMonoBinder : MonoViewMonoBinder<MonoView> { }
}
