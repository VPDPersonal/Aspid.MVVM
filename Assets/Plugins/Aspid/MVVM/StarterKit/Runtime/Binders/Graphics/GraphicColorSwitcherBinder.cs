#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class GraphicColorSwitcherBinder : SwitcherBinder<Color>
    {
        [Header("Component")]
        [SerializeField] private Graphic _graphic;

        public GraphicColorSwitcherBinder(Graphic graphic, Color trueColor, Color falseColor)
            : base(trueColor, falseColor)
        {
            _graphic = graphic ?? throw new ArgumentNullException(nameof(graphic));
        }

        protected override void SetValue(Color value) =>
            _graphic.color = value;
    }
}