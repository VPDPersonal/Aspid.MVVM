using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="MonoBinder"/> that binds an assigned <see cref="ScriptableObject"/> view to an <see cref="IViewModel"/>.
    /// </summary>
    /// <typeparam name="TView">The ScriptableObject view type.</typeparam>
    public abstract partial class ScriptableViewMonoBinder<TView> : MonoBinder, IBinder<IViewModel>
        where TView : ScriptableObject, IView
    {
        [Tooltip("The view initialized with the bound ViewModel.")]
        [SerializeField] private TView _view;

        private VersionStamp _valueVersion;

        /// <summary>
        /// Deinitializes the live view before applying the new ViewModel.
        /// </summary>
        /// <remarks>A newer value or detachment during deinitialization cancels this update.</remarks>
        /// <param name="viewModel">The new ViewModel. A <see langword="null"/> or destroyed ViewModel leaves the view deinitialized.</param>
        [BinderLog]
        public void SetValue(IViewModel viewModel)
        {
            _valueVersion = _valueVersion.Next();
            var version = _valueVersion;

            _view.DeinitializeViewSafely();

            if (version.Matches(_valueVersion))
                _view.InitializeSafely(viewModel);
        }

        /// <summary>
        /// Called before attachment to validate the assigned view.
        /// </summary>
        /// <exception cref="InvalidOperationException">The view is missing or destroyed.</exception>
        protected override void OnBinding()
        {
            if (_view == null)
                throw new InvalidOperationException("Assign a Scriptable View before binding.");
        }

        /// <summary>
        /// Called after removal to deinitialize the view.
        /// </summary>
        protected override void OnDetach()
        {
            _valueVersion = _valueVersion.Next();
            _view.DeinitializeViewSafely();
        }
    }

    /// <summary>
    /// Concrete <see cref="ScriptableViewMonoBinder{TView}"/> for <see cref="ScriptableView"/>.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Views/ScriptableView Binder")]
    public class ScriptableViewMonoBinder : ScriptableViewMonoBinder<ScriptableView> { }
}
