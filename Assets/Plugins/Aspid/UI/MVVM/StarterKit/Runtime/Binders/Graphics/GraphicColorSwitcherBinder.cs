using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders.Graphics
{
    public sealed class GraphicColorSwitcherBinder : SwitcherBinder<Color>
    {
        private readonly Graphic _graphic;

        public GraphicColorSwitcherBinder(Graphic graphic, Color trueColor, Color falseColor)
            : base(trueColor, falseColor)
        {
            _graphic = graphic;
        }

        protected override void SetValue(Color value) =>
            _graphic.color = value;
    }
}