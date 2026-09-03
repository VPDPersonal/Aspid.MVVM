#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ICanExecuteHandler"/> that switches a <see cref="Graphic.color"/> between two colors by the command state.
    /// </summary>
    [Serializable]
    public sealed class ColorCanExecuteHandler : ICanExecuteHandler
    {
        [Tooltip("Graphic whose color reflects the state.")]
        [SerializeField] private Graphic? _graphic;

        [Tooltip("Color applied when the command can execute.")]
        [SerializeField] private Color _trueColor = Color.white;

        [Tooltip("Color applied when the command cannot execute.")]
        [SerializeField] private Color _falseColor = Color.gray;

        private ColorCanExecuteHandler() { }

        /// <param name="graphic">The graphic whose color reflects the state.</param>
        /// <param name="trueColor">The color applied when the command can execute.</param>
        /// <param name="falseColor">The color applied when the command cannot execute.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="graphic"/> is <see langword="null"/>.</exception>
        public ColorCanExecuteHandler(
            Graphic graphic,
            Color trueColor,
            Color falseColor)
        {
            _trueColor = trueColor;
            _falseColor = falseColor;
            _graphic = graphic ?? throw new ArgumentNullException(nameof(graphic));
        }

        /// <inheritdoc/>
        public void SetCanExecute(bool canExecute)
        {
            if (_graphic) 
                _graphic.color = canExecute ? _trueColor : _falseColor;
        }
    }
}
