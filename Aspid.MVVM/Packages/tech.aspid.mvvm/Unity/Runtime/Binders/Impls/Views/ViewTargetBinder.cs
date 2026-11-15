#nullable enable
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that initializes the target view with the bound <see cref="IViewModel"/>
    /// and deinitializes it on unbind.
    /// </summary>
    /// <typeparam name="TView">The type of <see cref="Object"/> that implements <see cref="IView"/>.</typeparam>
    public abstract class ViewTargetBinder<TView> : TargetBinder<TView>, IBinder<IViewModel?>
        where TView : Object, IView
    {
        private uint _valueVersion;

        /// <summary>
        /// Creates a binder for the target view.
        /// </summary>
        /// <param name="target">The view to bind.</param>
        /// <param name="mode">The binding mode. Must be <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="target"/> is <see langword="null"/>.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The mode is <see cref="BindMode.None"/> or undefined.</exception>
        /// <exception cref="System.InvalidOperationException"><paramref name="mode"/> is not <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.</exception>
        protected ViewTargetBinder(
            TView target,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.EnsureForwardOnly();
        }

        /// <summary>
        /// Deinitializes the view, then initializes it with <paramref name="viewModel"/> unless it is <see langword="null"/>.
        /// </summary>
        /// <remarks>A newer value or detachment during deinitialization cancels this update.</remarks>
        /// <param name="viewModel">The ViewModel received from the binding, or <see langword="null"/> to deinitialize only.</param>
        public void SetValue(IViewModel? viewModel)
        {
            var version = unchecked(++_valueVersion);
            Target.Deinitialize();

            if (version != _valueVersion) return;
            if (viewModel is not null && Target)
                Target.Initialize(viewModel);
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

            Target.Deinitialize();
        }
    }
}
