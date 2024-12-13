using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/UI/Graphic/Graphic Binder - Color Enum")]
    public sealed class GraphicEnumColorGroupMonoBinder : EnumGroupMonoBinder<Graphic, Color>
    {
        protected override void SetValue(Graphic component, Color value) => 
            component.color = value;
    }
}