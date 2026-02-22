using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract <see cref="MonoBinder"/> that binds a serialized <typeparamref name="TView"/> ScriptableObject view
    /// to an <see cref="IViewModel"/> property.
    /// </summary>
    /// <typeparam name="TView">The type of the ScriptableObject view. Must inherit from <see cref="ScriptableObject"/> and implement <see cref="IView"/>.</typeparam>
    public abstract partial class ScriptableViewMonoBinder<TView> : MonoBinder, IBinder<IViewModel>
        where TView : ScriptableObject, IView
    {
        [SerializeField] private TView _view;

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
        /// Initializes the serialized view with the specified ViewModel.
        /// </summary>
        /// <param name="viewModel">The ViewModel to bind to the view.</param>
        protected void InitializeView(IViewModel viewModel) =>
            _view.Initialize(viewModel);

        /// <summary>
        /// Deinitializes the serialized view, detaching any bound ViewModel.
        /// </summary>
        protected void DeinitializeView() =>
            _view.Deinitialize();
    }

    /// <summary>
    /// Concrete <see cref="MonoBinder"/> for <see cref="ScriptableView"/> assets.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Views/ScriptableView Binder")]
    public class ScriptableViewMonoBinder : ScriptableViewMonoBinder<ScriptableView> { }
}