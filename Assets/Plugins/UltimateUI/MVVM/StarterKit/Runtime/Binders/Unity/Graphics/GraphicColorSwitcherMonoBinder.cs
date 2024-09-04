using UnityEngine;
using UnityEngine.UI;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Graphics
{
    [AddComponentMenu("UI/Binders/Graphic/Graphic Binder - Color Switcher")]
    public sealed class GraphicColorSwitcherMonoBinder : SwitcherMonoBinder<Graphic, Color>
    {
        protected override void SetValue(Color value) =>
            CachedComponent.color = value;
    }
}