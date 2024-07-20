using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Transforms
{
    [AddComponentMenu("UI/Binders/Transform/Transform Binder - Scale Switcher")]
    public partial class TransformScaleSwitcherBinder : TransformBinderBase, IBinder<bool>
    {
        [Header("Parameters")]
        [SerializeField] private Vector3 _trueScale;
        [SerializeField] private Vector3 _falseScale;

        protected Vector3 TrueScale => _trueScale;

        protected Vector3 FalseScale => _falseScale;

        [BinderLog]
        public void SetValue(bool value) =>
            CachedTransform.localScale = GetLocalScale(value);

        protected Vector3 GetLocalScale(bool value) =>
            value ? TrueScale : FalseScale;
    }
}