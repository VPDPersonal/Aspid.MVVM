using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class GameObjectTagSwitcherBinder : SwitcherBinder<string>
    {
        [Header("Component")]
        [SerializeField] private GameObject _gameObject;
        
        public GameObjectTagSwitcherBinder(GameObject gameObject, string trueValue, string falseValue)
            : base(trueValue, falseValue)
        {
            _gameObject = gameObject;
        }

        protected override void SetValue(string value) =>
            _gameObject.tag = value;
    }
}