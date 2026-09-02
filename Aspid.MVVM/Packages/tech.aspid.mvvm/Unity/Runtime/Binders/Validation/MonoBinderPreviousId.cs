using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Validation
{
    /// <summary>
    /// The last non-empty ID of a <see cref="MonoBinder"/>, kept to detect a renamed View field.
    /// </summary>
    [Serializable]
    public struct MonoBinderPreviousId
    {
        [Tooltip("The last non-empty ID.")]
        [SerializeField] private string _id;

        /// <summary>
        /// Gets the ID.
        /// </summary>
        public string Id => _id;

        /// <param name="id">The ID to keep.</param>
        public MonoBinderPreviousId(string id)
        {
            _id = id;
        }
    }
}
