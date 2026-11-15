#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="ComponentMonoBinder{TComponent}"/> that initializes the target view with the bound <see cref="IViewModel"/>.
    /// </summary>
    /// <typeparam name="TView">The type of <see cref="Component"/> that implements <see cref="IView"/>.</typeparam>
    public abstract partial class MonoViewMonoBinder<TView> : ComponentMonoBinder<TView>, IBinder<IViewModel?>
        where TView : Component, IView
    {
        private uint _valueVersion;

        /// <summary>
        /// Deinitializes the view, then initializes it with <paramref name="viewModel"/> unless it is <see langword="null"/>.
        /// </summary>
        /// <remarks>A newer value or detachment during deinitialization cancels this update.</remarks>
        /// <param name="viewModel">The ViewModel received from the binding, or <see langword="null"/> to deinitialize only.</param>
        [BinderLog]
        public void SetValue(IViewModel? viewModel)
        {
            var version = unchecked(++_valueVersion);
            CachedComponent.Deinitialize();

            if (version != _valueVersion) return;
            if (viewModel is not null && CachedComponent && this)
                CachedComponent.Initialize(viewModel);
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

            CachedComponent.Deinitialize();
        }
    }

    /// <summary>
    /// Concrete <see cref="MonoViewMonoBinder{TView}"/> for <see cref="MonoView"/>.
    /// </summary>
    [AddBinderContextMenu(typeof(MonoView))]
    [AddComponentMenu("Aspid/MVVM/Binders/Views/MonoView Binder")]
    public class MonoViewMonoBinder : MonoViewMonoBinder<MonoView> { }
}
