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
        public override IViewModel ViewModel => _viewModel;

        /// <summary>
        /// Initializes all views with <paramref name="viewModel"/>.
        /// </summary>
        /// <param name="viewModel">The ViewModel to bind.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="viewModel"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the views are already initialized.</exception>
        public void Initialize(IViewModel viewModel)
        {
            if (IsInitialized)
                throw new InvalidOperationException($"{name}: views are already initialized");

            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            foreach (var view in Views)
                view.Initialize(viewModel);

            IsInitialized = true;
        }

        /// <summary>
        /// Deinitializes all views. Does nothing when they are not initialized.
        /// </summary>
        public void Deinitialize()
        {
            if (!IsInitialized) return;

            foreach (var view in Views)
                view.Deinitialize();

            _viewModel = null;
            IsInitialized = false;
        }
    }
}
