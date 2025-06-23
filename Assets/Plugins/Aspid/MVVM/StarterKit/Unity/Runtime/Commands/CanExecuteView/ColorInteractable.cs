using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Unity
{
    [Serializable]
    public sealed class ColorInteractable : ICanExecuteView
    {
        [SerializeField] private Graphic _graphic;
        
        [SerializeField] private Color _trueColor;
        [SerializeField] private Color _falseColor;
        
        public ColorInteractable(Graphic graphic, Color trueColor, Color falseColor)
        {
            _graphic = graphic;
            _trueColor = trueColor;
            _falseColor = falseColor;
        }
        
        public void SetCanExecute(bool canExecute) =>
            _graphic.color = canExecute ? _trueColor : _falseColor;
    }
}