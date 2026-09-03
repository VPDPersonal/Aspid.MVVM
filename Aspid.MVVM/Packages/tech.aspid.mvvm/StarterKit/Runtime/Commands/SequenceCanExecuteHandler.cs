#nullable enable
using System;
using UnityEngine;
using Aspid.FastTools.Types;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ICanExecuteHandler"/> that forwards the state to every nested handler in order.
    /// </summary>
    [Serializable]
    public sealed class SequenceCanExecuteHandler : ICanExecuteHandler
    {
        [Tooltip("Handlers that receive the state, in order. Empty slots are skipped.")]
        [TypeSelector]
        [SerializeReference] private ICanExecuteHandler?[] _handlers = Array.Empty<ICanExecuteHandler>();

        private SequenceCanExecuteHandler() { }

        /// <param name="handlers">The handlers that receive the state, in order.</param>
        public SequenceCanExecuteHandler(params ICanExecuteHandler?[]? handlers)
        {
            _handlers = handlers ?? Array.Empty<ICanExecuteHandler>();
        }

        /// <inheritdoc/>
        public void SetCanExecute(bool canExecute)
        {
            foreach (var handler in _handlers)
                handler?.SetCanExecute(canExecute);
        }
    }
}
