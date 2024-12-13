using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Graphic/Graphic Binder - Color Enum")]
    public sealed class GraphicEnumColorMonoBinder : EnumMonoBinder<Graphic, Color>
    {
        protected override void SetValue(Color value) =>
            CachedComponent.color = value;
    }
}