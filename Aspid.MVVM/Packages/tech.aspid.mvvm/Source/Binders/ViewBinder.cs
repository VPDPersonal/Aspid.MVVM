#if ENABLE_PROFILER && !ASPID_MVVM_UNITY_PROFILER_DISABLED
#define ASPID_MVVM_PROFILER
#endif

using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// <see cref="Binder"/> that initializes an <see cref="IView"/> with each ViewModel it receives.
    /// </summary>
    [SuppressMessage(category: "ReSharper", checkId: "MemberCanBePrivate.Global")]
    public class ViewBinder : Binder, IBinder<IViewModel?>
    {
        /// <summary>
        /// The view the binder initializes.
        /// </summary>
        protected readonly IView View;
        private VersionStamp _valueVersion;

        /// <summary>
        /// Creates a binder for <paramref name="view"/>.
        /// </summary>
        /// <param name="view">The view to bind.</param>
        /// <exception cref="ArgumentNullException"><paramref name="view"/> is <see langword="null"/>.</exception>
        public ViewBinder(IView view)
        {
            View = view ?? throw new ArgumentNullException(nameof(view));
        }

        /// <summary>
        /// Deinitializes <see cref="View"/> and initializes it with <paramref name="viewModel"/>.
        /// </summary>
        /// <remarks>A newer value or detachment during deinitialization cancels this update.</remarks>
        /// <param name="viewModel">The ViewModel received from the bindable member; <see langword="null"/> leaves the view deinitialized.</param>
        public void SetValue(IViewModel? viewModel)
        {
#if ASPID_MVVM_PROFILER
            using (this.Marker())
#endif
            {
                _valueVersion = _valueVersion.Next();
                var version = _valueVersion;

                View.DeinitializeViewSafely();

                if (version.Matches(_valueVersion))
                    View.InitializeSafely(viewModel);
            }
        }
        
        /// <summary>
        /// Called before attachment to validate the required view.
        /// </summary>
        /// <remarks>Overrides must call <c>base.OnBinding()</c> before acquiring resources.</remarks>
        /// <exception cref="InvalidOperationException">The view is missing or destroyed.</exception>
        protected override void OnBinding()
        {
            if (View.IsMissing())
                throw new InvalidOperationException("Assign a valid view before binding.");
        }

        /// <summary>
        /// Called after the binder detaches to deinitialize <see cref="View"/> and cancel any pending value update.
        /// </summary>
        protected override void OnDetach()
        {
            _valueVersion = _valueVersion.Next();
            View.DeinitializeViewSafely();
        }
    }
}
