#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Validation
{
    /// <summary>
    /// The last non-empty View of a <see cref="MonoBinder"/>, kept with its name to detect a lost reference.
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
        /// <param name="view">The View to keep; <see langword="null"/> records an empty name.</param>
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
        /// Gets the recorded View, or <see langword="null"/> for none; the recorded object may have been destroyed.
        /// </summary>
        public Component? View => _view;
    }
}
