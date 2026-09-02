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
        [SerializeField] private string _name;

        [Tooltip("The last non-empty View.")]
        [SerializeField] private Component _view;

        /// <summary>
        /// Gets the name the View had when it was set.
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Gets the View.
        /// </summary>
        public Component View => _view;

        /// <param name="view">The View to keep.</param>
        public MonoBinderPreviousView(Component view)
        {
            _view = view;
            _name = view ? view.name : string.Empty;
        }
    }
}
