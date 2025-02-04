#nullable enable
using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class GameObjectTagSwitcherBinder : SwitcherBinder<GameObject, string>
    {
        public GameObjectTagSwitcherBinder(
            GameObject target,
            string trueValue,
            string falseValue, 
            BindMode mode = BindMode.OneWay)
            : base(target, trueValue, falseValue, mode) { }

        protected override void SetValue(string value) =>
            Target.tag = value;
    }
}