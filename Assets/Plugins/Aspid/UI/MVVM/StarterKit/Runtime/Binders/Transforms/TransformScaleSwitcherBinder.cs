using UnityEngine;

namespace Aspid.UI.MVVM.StarterKit.Binders.Transforms
{
    public sealed class TransformScaleSwitcherBinder : SwitcherBinder<Vector3>
    {
        private readonly Transform _transform;

        public TransformScaleSwitcherBinder(Transform transform, Vector3 trueValue, Vector3 falseValue) 
            : base(trueValue, falseValue)
        {
            _transform = transform;
        }

        protected override void SetValue(Vector3 value) =>
            _transform.localScale = value;
    }
}