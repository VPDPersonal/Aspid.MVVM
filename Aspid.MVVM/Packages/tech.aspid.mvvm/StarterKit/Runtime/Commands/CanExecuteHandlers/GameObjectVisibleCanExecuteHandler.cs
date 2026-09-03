#nullable enable
using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ICanExecuteHandler"/> that toggles a <see cref="GameObject"/> active by the command state.
    /// </summary>
    [Serializable]
    public sealed class GameObjectVisibleCanExecuteHandler : ICanExecuteHandler
    {
        [Tooltip("GameObject whose active state reflects the command state.")]
        [SerializeField] private GameObject? _gameObject;

        [Tooltip("Optional converter applied to the state; empty leaves it as-is.")]
        [SerializeReference] private IConverter<bool, bool>? _converter;

        private GameObjectVisibleCanExecuteHandler() { }

        /// <param name="gameObject">The GameObject whose active state reflects the command state.</param>
        /// <param name="converter">The converter applied to the state, or <see langword="null"/> to use it unchanged.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="gameObject"/> is <see langword="null"/>.</exception>
        public GameObjectVisibleCanExecuteHandler(
            GameObject gameObject,
            IConverter<bool, bool>? converter = null)
        {
            _converter = converter;
            _gameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
        }

        /// <inheritdoc/>
        public void SetCanExecute(bool canExecute)
        {
            if (_gameObject)
                _gameObject.SetActive(_converter?.Convert(canExecute) ?? canExecute);
        }
    }
}
