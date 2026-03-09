using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that holds a serialized <typeparamref name="TView"/> <see cref="ScriptableObject"/> view
    /// and initializes it when a bound <see cref="IViewModel"/> is received.
    /// </summary>
    /// <typeparam name="TView">The type of <see cref="ScriptableObject"/> that implements <see cref="IView"/>.</typeparam>
    public abstract partial class ScriptableViewMonoBinder<TView> : MonoBinder, IBinder<IViewModel>
        where TView : ScriptableObject, IView
    {
        [Tooltip("The ScriptableObject view to initialize with the bound ViewModel.")]
        [SerializeField] private TView _view;

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
    /// <see cref="ScriptableViewMonoBinder{TView}"/> specialized for <see cref="ScriptableView"/> assets.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Views/ScriptableView Binder")]
    public class ScriptableViewMonoBinder : ScriptableViewMonoBinder<ScriptableView> { }
}