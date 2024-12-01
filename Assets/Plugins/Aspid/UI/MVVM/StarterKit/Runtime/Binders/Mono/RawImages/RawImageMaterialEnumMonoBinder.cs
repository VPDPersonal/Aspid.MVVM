using UnityEngine;
using UnityEngine.UI;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Raw Image/Raw Image Binder - Material Enum")]
    public sealed class RawImageMaterialEnumMonoBinder : EnumMonoBinder<RawImage, Material>
    {
        protected override void SetValue(Material value) =>
            CachedComponent.material = value;
    }
}