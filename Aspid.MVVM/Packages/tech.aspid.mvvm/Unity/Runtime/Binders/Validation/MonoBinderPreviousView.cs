#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Validation
{
    /// <summary>
    /// Record of a <see cref="MonoBinder"/> view and its name for detecting a lost reference.
    /// </summary>
    [Serializable]
    public struct MonoBinderPreviousView
    {
        [Tooltip("The name the View had when it was set.")]
        [SerializeField] private string? _name;

        [Tooltip("The last non-empty View.")]
        [SerializeField] private Component? _view;

        /// <summary>
        /// Creates a record of the supplied View and its current name.
        /// </summary>
        /// <param name="view">The view to record, or <see langword="null"/> to record an empty name.</param>
        public MonoBinderPreviousView(Component? view)
        {
            _view = view;
            _name = view ? view.name : string.Empty;
        }

        /// <summary>
        /// Gets the recorded name, or <see langword="null"/> for a default record.
        /// </summary>
        public string? Name => _name;

        /// <summary>
        /// Gets the recorded view, which may have been destroyed, or <see langword="null"/> when unassigned.
        /// </summary>
        public Component? View => _view;
    }
}
