using UnityEngine;

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Canvas Group/CanvasGroup Binder - BlocksRaycasts")]
    public partial class CanvasGroupBlocksRaycastsMonoBinder : ComponentMonoBinder<CanvasGroup>, IBinder<bool>
    {
        [Header("Converter")]
        [SerializeField] private bool _isInvert;
        
        [BinderLog]
        public void SetValue(bool value) =>
            CachedComponent.blocksRaycasts = _isInvert ? !value : value;
    }
}