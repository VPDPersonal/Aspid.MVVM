using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ViewInitializerBase"/> that takes its ViewModel from an explicit <see cref="Initialize"/> call.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/View Initializers/View Initializer Manual")]
    [AddBinderContextMenu(typeof(MonoView), Path = "Add View Initializers/View Initializer Manual")]
    public sealed class ViewInitializerManual : ViewInitializerBase
    {
        private IViewModel _viewModel;

        /// <inheritdoc/>
        public override IViewModel ViewModel =>
            _viewModel;

        /// <summary>
        /// Initializes all views with <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="viewModel"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">The views are already initialized or initialization is in progress.</exception>
        public void Initialize(IViewModel viewModel)
        {
            EnsureNotInitializing();
            if (IsInitialized)
                throw new InvalidOperationException($"{name}: views are already initialized");

            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            try
            {
                InitializeViews(viewModel);
            }
            catch
            {
                _viewModel = null;
                throw;
            }
        }

        /// <summary>
        /// Deinitializes all views. Does nothing when they are not initialized.
        /// </summary>
        /// <exception cref="InvalidOperationException">Initialization is in progress.</exception>
        /// <exception cref="AggregateException">Multiple views fail to deinitialize.</exception>
        public void Deinitialize()
        {
            EnsureNotInitializing();
            try
            {
                DeinitializeViews();
            }
            finally
            {
                _viewModel = null;
            }
        }
    }
}
