using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="CanvasGroup.blocksRaycasts"/> property on a <see cref="CanvasGroup"/>
    /// to a value resolved from an enum bound on the ViewModel.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/UI/CanvasGroup/CanvasGroup Binder – BlocksRaycasts Enum")]
    [AddBinderContextMenu(typeof(CanvasGroup), serializePropertyNames: "m_BlocksRaycasts", SubPath = "Enum")]
    public sealed class CanvasGroupBlocksRaycastsEnumMonoBinder : EnumMonoBinder<CanvasGroup, bool>
    {
        protected override void SetValue(bool value) =>
            CachedComponent.blocksRaycasts = value;
    }
}