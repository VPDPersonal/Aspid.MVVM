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
        [SerializeField] private Graphic _graphic;

        [Tooltip("Color applied when the command can execute.")]
        [SerializeField] private Color _trueColor;

        [Tooltip("Color applied when the command cannot execute.")]
        [SerializeField] private Color _falseColor;

        private ColorCanExecuteHandler() { }

        /// <param name="graphic">The graphic whose color reflects the state.</param>
        /// <param name="trueColor">The color applied when the command can execute.</param>
        /// <param name="falseColor">The color applied when the command cannot execute.</param>
        public ColorCanExecuteHandler(Graphic graphic, Color trueColor, Color falseColor)
        {
            _graphic = graphic;
            _trueColor = trueColor;
            _falseColor = falseColor;
        }

        /// <inheritdoc/>
        public void SetCanExecute(bool canExecute) =>
            _graphic.color = canExecute ? _trueColor : _falseColor;
    }
}
