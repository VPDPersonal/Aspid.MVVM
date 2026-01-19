using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Behaviour))]
    [AddComponentMenu("Aspid/MVVM/Binders/Behaviour/Behaviour Binder – Enabled Enum")]
    public sealed class BehaviourEnabledEnumMonoBinder : EnumMonoBinder<Behaviour, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.enabled = value;
    }
}