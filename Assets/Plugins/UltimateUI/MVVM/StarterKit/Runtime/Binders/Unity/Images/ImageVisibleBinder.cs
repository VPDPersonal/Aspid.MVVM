using UnityEngine;
using UnityEngine.UI;
using UltimateUI.MVVM.Unity.Generation;

namespace UltimateUI.MVVM.StarterKit.Binders.Unity.Images
{
    [AddComponentMenu("UI/Binders/Image/Image Binder - Visible")]
    public partial class ImageVisibleBinder : ComponentMonoBinder<Image>, IBinder<bool>
    {
        [field: Header("Parameters")]
        [field: SerializeField] protected bool IsInvert { get; private set; }
        
        [BinderLog]
        public void SetValue(bool value)
        {
            if (IsInvert) value = !IsInvert;
            CachedComponent.enabled = value;
        }
    }
}