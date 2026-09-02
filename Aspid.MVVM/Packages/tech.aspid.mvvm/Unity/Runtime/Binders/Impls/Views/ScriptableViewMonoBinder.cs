using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Abstract base <see cref="MonoBinder"/> that initializes a serialized <see cref="ScriptableObject"/> view with the bound <see cref="IViewModel"/>.
    /// </summary>
    /// <typeparam name="TView">The type of <see cref="ScriptableObject"/> that implements <see cref="IView"/>.</typeparam>
    public abstract partial class ScriptableViewMonoBinder<TView> : MonoBinder, IBinder<IViewModel>
        where TView : ScriptableObject, IView
    {
        [Tooltip("The view initialized with the bound ViewModel.")]
        [SerializeField] private TView _view;

        /// <summary>
        /// Indicates whether binding is allowed: <see langword="false"/> when no view is assigned.
        /// </summary>
        public override bool CanBind => _view;

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
        /// Initializes the view with <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel to initialize the view with.</param>
        protected void InitializeView(IViewModel viewModel) =>
            _view.Initialize(viewModel);

        /// <summary>
        /// Deinitializes the view.
        /// </summary>
        protected void DeinitializeView() =>
            _view.Deinitialize();
    }

    /// <summary>
    /// <see cref="ScriptableViewMonoBinder{TView}"/> for <see cref="ScriptableView"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Views/ScriptableView Binder")]
    public class ScriptableViewMonoBinder : ScriptableViewMonoBinder<ScriptableView> { }
}
