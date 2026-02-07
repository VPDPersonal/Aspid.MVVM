using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenu(typeof(Behaviour), SubPath = "EnumGroup")]
    [AddComponentMenu("Aspid/MVVM/Binders/Behaviour/Behaviour Binder – Enabled EnumGroup")]
    public sealed class BehaviourEnabledEnumGroupMonoBinder : EnumGroupMonoBinder<Behaviour, bool>
    {
        protected override void SetValue(Behaviour element, bool value) =>
            element.enabled = value;
    }
}