#nullable enable
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="MonoBinder"/> that initializes a serialized <see cref="ScriptableObject"/> view with the bound <see cref="IViewModel"/>.
    /// </summary>
    /// <typeparam name="TView">The type of <see cref="ScriptableObject"/> that implements <see cref="IView"/>.</typeparam>
    [SuppressMessage(category: "ReSharper", checkId: "NullableWarningSuppressionIsUsed")]
    public abstract partial class ScriptableViewMonoBinder<TView> : MonoBinder, IBinder<IViewModel?>
        where TView : ScriptableObject, IView
    {
        [Tooltip("The view initialized with the bound ViewModel.")]
        [SerializeField] private TView? _view;

        private uint _valueVersion;

        /// <summary>
        /// Gets a value indicating whether binding is allowed: <see langword="false"/> when no view is assigned.
        /// </summary>
        public override bool CanBind => _view;

        /// <summary>
        /// Deinitializes the view, then initializes it with <paramref name="viewModel"/> unless it is <see langword="null"/>.
        /// </summary>
        /// <remarks>A newer value or detachment during deinitialization cancels this update.</remarks>
        /// <param name="viewModel">The ViewModel received from the binding, or <see langword="null"/> to deinitialize only.</param>
        [BinderLog]
        public void SetValue(IViewModel? viewModel)
        {
            var version = unchecked(++_valueVersion);
            _view!.Deinitialize();

            if (version != _valueVersion) return;
            if (viewModel is not null && _view && this)
                _view!.Initialize(viewModel);
        }

        /// <summary>
        /// Called after removal to deinitialize the view and cancel any pending value update.
        /// </summary>
        protected override void OnDetach()
        {
            unchecked
            {
                ++_valueVersion;
            }

            _view!.Deinitialize();
        }
    }

    /// <summary>
    /// Concrete <see cref="ScriptableViewMonoBinder{TView}"/> for <see cref="ScriptableView"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Views/ScriptableView Binder")]
    public class ScriptableViewMonoBinder : ScriptableViewMonoBinder<ScriptableView> { }
}
