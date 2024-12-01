using UnityEngine;
using UnityEngine.UI;
using Aspid.UI.MVVM.Mono.Generation;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/Raw Image/Raw Image Binder - Material")]
    public partial class RawImageMaterialMonoBinder : ComponentMonoBinder<RawImage>, IBinder<Material>
    {
        [BinderLog]
        public void SetValue(Material value) =>
            CachedComponent.material = value;
    }
}