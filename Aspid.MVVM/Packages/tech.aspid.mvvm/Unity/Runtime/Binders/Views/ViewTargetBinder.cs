#nullable enable
#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="TargetBinder{TTarget}"/> that binds a view to an <see cref="IViewModel"/> and deinitializes it on unbind.
    /// </summary>
    /// <typeparam name="TView">The Unity view type.</typeparam>
    public abstract class ViewTargetBinder<TView> : TargetBinder<TView>, IBinder<IViewModel?>
        where TView : Object, IView
    {
        private VersionStamp _valueVersion;

        /// <summary>
        /// Creates a forward-only view binder.
        /// </summary>
        /// <param name="target">The view to bind.</param>
        /// <param name="mode">The binding mode. Must be <see cref="BindMode.OneWay"/> or <see cref="BindMode.OneTime"/>.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="mode"/> is <see cref="BindMode.None"/> or undefined.</exception>
        /// <exception cref="System.ArgumentNullException"><paramref name="target"/> is <see langword="null"/>.</exception>
        /// <exception cref="System.InvalidOperationException"><paramref name="mode"/> includes reverse binding.</exception>
        protected ViewTargetBinder(
            TView target,
            BindMode mode = BindMode.OneWay)
            : base(target, mode)
        {
            mode.EnsureForwardOnly();
        }

        /// <summary>
        /// Deinitializes the live view before applying the new ViewModel.
        /// </summary>
        /// <remarks>A newer value or detachment during deinitialization cancels this update.</remarks>
        /// <param name="viewModel">The new ViewModel. A <see langword="null"/> or destroyed ViewModel leaves the view deinitialized.</param>
        public void SetValue(IViewModel? viewModel)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                _valueVersion = _valueVersion.Next();
                var version = _valueVersion;

                Target.DeinitializeViewSafely();

                if (version.Matches(_valueVersion))
                    Target.InitializeSafely(viewModel);
            }
        }

        /// <summary>
        /// Called after removal to deinitialize the view.
        /// </summary>
        protected override void OnDetach()
        {
            _valueVersion = _valueVersion.Next();
            Target.DeinitializeViewSafely();
        }
    }
}
