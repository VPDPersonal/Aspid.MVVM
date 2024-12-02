#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    public sealed class CanvasGroupAlphaSwitcherBinder : SwitcherBinder<float>
    {
        private readonly CanvasGroup _canvasGroup;

        public CanvasGroupAlphaSwitcherBinder(CanvasGroup canvasGroup, float trueValue, float falseValue) 
            : base(trueValue, falseValue)
        {
            if (trueValue is < 0 or > 1) throw new ArgumentException($"{nameof(trueValue)} must be between 0 and 1.");
            if (falseValue is < 0 or > 1) throw new ArgumentException($"{nameof(falseValue)} must be between 0 and 1.");

            _canvasGroup = canvasGroup ?? throw new ArgumentNullException(nameof(canvasGroup));
        }

        protected override void SetValue(float value) => 
            _canvasGroup.alpha = value;
    }
}