#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Validation
{
    /// <summary>
    /// Record of a <see cref="MonoBinder"/> field ID for detecting renames.
    /// </summary>
    [Serializable]
    public struct MonoBinderPreviousId
    {
        [Tooltip("The last non-empty ID.")]
        [SerializeField] private string? _id;

        /// <summary>
        /// Creates a record of the supplied ID.
        /// </summary>
        /// <param name="id">The ID to keep, or <see langword="null"/> for none.</param>
        public MonoBinderPreviousId(string? id)
        {
            _id = id;
        }

        /// <summary>
        /// Gets the ID, or <see langword="null"/> when no ID has been recorded.
        /// </summary>
        public string? Id => _id;
    }
}
