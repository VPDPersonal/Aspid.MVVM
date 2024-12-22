using System;
using UnityEngine;

namespace Aspid.MVVM.StarterKit.Binders
{
    [Serializable]
    public sealed class GameObjectTagSwitcherBinder : SwitcherBinder<string>
    {
        [Header("Component")]
        [SerializeField] private GameObject _gameObject;
        
        public GameObjectTagSwitcherBinder(string trueValue, string falseValue, GameObject gameObject)
            : base(trueValue, falseValue)
        {
            _gameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));
        }

        protected override void SetValue(string value) =>
            _gameObject.tag = value;
    }
}