using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Graphic/Graphic Binder - Color Enum")]
    public sealed class GraphicEnumColorMonoBinder : EnumMonoBinder<Graphic, Color>
    {
        protected override void SetValue(Color value) =>
            CachedComponent.color = value;
    }
}