using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that binds a component view to an <see cref="IViewModel"/>.
    /// </summary>
    /// <typeparam name="TView">The component view type.</typeparam>
    public abstract partial class MonoViewMonoBinder<TView> : ComponentMonoBinder<TView>, IBinder<IViewModel>
        where TView : Component, IView
    {
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

            var view = CachedComponent;
            view.DeinitializeViewSafely();

            if (version.Matches(_valueVersion))
                view.InitializeSafely(viewModel);
        }

        /// <summary>
        /// Called after removal to deinitialize the view.
        /// </summary>
        protected override void OnDetach()
        {
            _valueVersion = _valueVersion.Next();
            CachedComponent.DeinitializeViewSafely();
        }
    }

    /// <summary>
    /// Concrete <see cref="MonoViewMonoBinder{TView}"/> for <see cref="MonoView"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(MonoView))]
    [AddComponentMenu("Aspid/MVVM/Binders/Views/MonoView Binder")]
    public class MonoViewMonoBinder : MonoViewMonoBinder<MonoView> { }
}
