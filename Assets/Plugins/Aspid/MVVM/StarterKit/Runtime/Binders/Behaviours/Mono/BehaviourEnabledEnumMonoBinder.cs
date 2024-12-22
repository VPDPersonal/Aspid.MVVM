using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/Behaviour/Behaviour Binder - Enabled Enum")]
    public sealed class BehaviourEnabledEnumMonoBinder : EnumComponentMonoBinder<Behaviour, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.enabled = value;
    }
}